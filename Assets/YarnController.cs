using UnityEngine;

public class YarnController : MonoBehaviour
{
    [SerializeField, Range(0,25f)] private float _movementSpeed;
    [SerializeField] private Vector2 _mousePosition;
    private Vector2 _targetPosition;
    
    void Update()
    {
        MoveYarn();
        // RotateYarnHead(_mousePosition);
    }

    // private void RotateYarnHead(Vector2 targetPosition)
    // {
    //     float FetchYarnDegree(Vector3 targetPosition)
    //     {
    //         Vector3 Direction() 
    //             => (targetPosition - transform.position).normalized;
    //         
    //         return Vector2.Angle(transform.up ,Direction());
    //     }
    //
    //     transform.localEulerAngles = 
    //         new Vector3(transform.rotation.x,transform.rotation.y,FetchYarnDegree(targetPosition));
    // }
    //
    private void MoveYarn()
    {
        bool CameraNull() => !Camera.main;

        if (CameraNull()) return;
        
        var worldMousePosition = Camera.main.ScreenToWorldPoint(
            new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        var newYarnPosition = 
            (worldMousePosition - transform.position).normalized * _movementSpeed;

        _mousePosition = worldMousePosition;
        
        transform.position += newYarnPosition * Time.deltaTime;
    }
}
