using System;
using System.Collections;
using Mono.Cecil.Cil;
using Unity.Mathematics;
using Unity.VisualScripting;
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
    private TrailRenderer _trail;
    public GameObject trail;

    private Vector2 startPosition;
    [Space(15f), Range(0f,65f)] public float globeRadius;

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
        if (trailInstance) return;
        trailInstance = Instantiate(trail, transform.position, quaternion.identity, transform);
    }

    public bool GetMoveStatus() 
        => _canMove;

    public Vector3 GetDirection()
        => (Camera.main.ScreenToWorldPoint(
            new Vector2(
                Input.mousePosition.x, 
                Input.mousePosition.y)) 
            - 
            transform.position).normalized;
    
    public void SetNewYarnPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public void CreateStringTrail()
    {
        trailInstance = Instantiate(trail, transform.position, quaternion.identity, transform);
        _trail = trailInstance.GetComponent<TrailRenderer>();
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
            new Vector3(Input.mousePosition.x, Input.mousePosition.y,-10f));

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

        var plantOffset = new Vector3(globeRadius, globeRadius);
        
        var placePoint = transform.position + plantOffset;
        
        Vector3 diff = transform.position - Vector3.zero;
        
        diff.Normalize();
 
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        
        GameObject plant = Instantiate(mushroomGameObject, placePoint, Quaternion.identity);
        
        plant.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        CancelMovement();
    }
    private void CancelMovement()
    {
        _canMove = false;
        if (trailInstance != null)
            trailInstance.GetComponent<TrailRenderer>().emitting = false;
        UnMountStringTrail();
        StartCoroutine(ResetPosition());
    }
    private IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(1f);
        SetNewYarnPosition(startPosition);
        GameStateController.Instance.ChangeToManagerState();
    }
    private void Instance_OnManagementStateEnter()
    {
        CancelMovement();
    }
    private void Instance_OnConquerStateEnter()
    {
        _canMove = true;
    }
}
