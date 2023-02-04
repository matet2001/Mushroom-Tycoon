using System;
using Mono.Cecil.Cil;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class YarnController : MonoBehaviour
{
    [SerializeField, Range(0,25f)] private float _movementSpeed;
    [SerializeField, Range(1, 25f)] private float _sprintSpeed = 1;
    [SerializeField] private Vector2 _mousePosition;
    private Vector2 _targetPosition;
    private bool _canMove;
    [Space] 
    public string obstacleGOTag;

    public string resourceGOTag;
    [Space] 
    private GameObject trailInstance;
    public GameObject trail;

    private Vector2 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }
    private void Start()
    {
        CreateStringTrail();

        GameStateController.Instance.OnConquerStateEnter += Instance_OnConquerStateEnter;
        GameStateController.Instance.OnManagementStateEnter += Instance_OnManagementStateEnter;
    }
    void Update()
    {
        if (!_canMove) return;
        MoveYarn();
    }

    public void SetNewYarnPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public void CreateStringTrail()
    {
        trailInstance = Instantiate(trail, transform.position, quaternion.identity, transform);
    }

    public void UnMountStringTrail()
    {
        if (trailInstance == null) return;
        
        trailInstance.transform.parent = null;
        trailInstance = null;
    }

    private void MoveYarn()
    {
        // Functions
        bool CameraNull() => !Camera.main;

        bool IsHoldingSprintInput()
            => !Input.GetMouseButton(0);
        
        // Logic

        if (CameraNull()) return;
        
        var worldMousePosition = Camera.main.ScreenToWorldPoint(
            new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        var newYarnPosition = 
            (worldMousePosition - transform.position).normalized * _movementSpeed;

        _mousePosition = worldMousePosition;

        switch (IsHoldingSprintInput())
        {
            case true:
                transform.position += newYarnPosition * Time.deltaTime;
                break;
            case false:
                transform.position += newYarnPosition * (Time.deltaTime * _sprintSpeed);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out CollidableBase collidableBase))
        {
            collidableBase.Collision();
            GameStateController.Instance.FireOnManagmentStateEnter();
        }         
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Earth")) return;
        GameObject mushroomGameObject = Resources.Load<GameObject>("PfMushroom");

        Bounds bounds = collision.GetComponent<SpriteRenderer>().bounds;
        
        Vector2 earthWidthVector = new Vector2(bounds.extents.x, 0f);
        Debug.DrawLine(bounds.center, (Vector2)bounds.center + earthWidthVector, Color.red, 100f);
        Vector2 earthRotationVector = (transform.position - collision.transform.position).normalized;
        Vector2 earthWidthVectorRotated = earthRotationVector * earthWidthVector.magnitude;
        Vector2 placePoint = (Vector2)bounds.center + earthWidthVectorRotated;
        Debug.DrawLine(bounds.center, (Vector2)bounds.center + earthWidthVectorRotated, Color.blue, 100f);
        Quaternion placeRotation = Quaternion.FromToRotation(Vector2.up, earthRotationVector);

        Instantiate(mushroomGameObject, placePoint, Quaternion.identity);
        CancelMovement();
    }
    private void CancelMovement()
    {
        UnMountStringTrail();
        _canMove = false;
        transform.position = startPosition;
        GameStateController.Instance.ChangeToManagerState();
    }
    private void Instance_OnManagementStateEnter()
    {
        CancelMovement();
    }
    private void Instance_OnConquerStateEnter()
    {
        _canMove = true;
        trailInstance = Instantiate(trail, transform.position, quaternion.identity, transform);
    }
}
