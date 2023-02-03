using System;
using UnityEngine;

public class YarnController : MonoBehaviour
{
    [SerializeField, Range(0,25f)] private float _movementSpeed;
    [SerializeField, Range(1, 25f)] private float _sprintSpeed = 1;
    [SerializeField] private Vector2 _mousePosition;
    private Vector2 _targetPosition;
    private bool canMove;

    private void Start()
    {
        canMove = true;
    }

    void Update()
    {
        if (!canMove) return;
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
                //TODO: resource csökkentése
                break;
            case false:
                transform.position += newYarnPosition * (Time.deltaTime * _sprintSpeed);
                //TODO: ide majd valami olyasmi kell, hogy:  resoure -= x
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //TODO: kicserélni egy állítható layerre
        bool HasCollidedWithObstacle() => col.gameObject.layer == 2;

        if (!HasCollidedWithObstacle()) return;
        
        canMove = false;
    }
}
