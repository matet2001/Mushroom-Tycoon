using System;
using Mono.Cecil.Cil;
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

    private void Start()
    {
        _canMove = true;
    }

    void Update()
    {
        if (!_canMove) return;
        MoveYarn();
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
                ResourceManager.Instance.SubstractResourceAmountAll(1);
                break;
            case false:
                transform.position += newYarnPosition * (Time.deltaTime * _sprintSpeed);
                ResourceManager.Instance.SubstractResourceAmountAll(2);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        bool HasCollidedWithObstacle() => col.gameObject.CompareTag(obstacleGOTag); //Obstacle layer

        bool HasCollidedWithResource() => col.gameObject.CompareTag(resourceGOTag); //Resource layer

        if (HasCollidedWithResource())
            Debug.Log("Collided with resource");
        
        if (!HasCollidedWithObstacle()) return;
        
        _canMove = false;
    }
}
