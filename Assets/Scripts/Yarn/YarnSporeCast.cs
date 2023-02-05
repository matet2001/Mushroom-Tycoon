using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class YarnSporeCast : MonoBehaviour
{
    private YarnController _yarnController;
    [Space] public SporeRootInstance sporeInstance;
    private SporeRootInstance _spore;
    [Space, Range(0f, 15f)] public float spawnCircleRadius;
    [Space, Range(0f, 15f)] public float maximumTimeBtwCast;
    private float _timeBtwCast;
    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        _yarnController = GetComponent<YarnController>();
        _timeBtwCast = maximumTimeBtwCast;
    }
    
    void Update()
    {
        _timeBtwCast -= Time.deltaTime;
        if (!_yarnController.GetMoveStatus()) ResetCastTimer();
        if (!_yarnController.GetMoveStatus()) return;

        _direction = _yarnController.GetDirection();

        if (_timeBtwCast > 0) return;
        
        var sporeRootGoalPosition
            = (
                new Vector3
                (
                    Random.Range(
                        (
                            transform.position.x - spawnCircleRadius
                        )
                        +
                        (
                            -_direction.x * 2
                        )
                        ,
                        (
                            transform.position.x + spawnCircleRadius
                        )
                        +
                        (
                            -_direction.x * 2
                        )
                    )
                    ,
                    Random.Range(
                        (
                            transform.position.y - spawnCircleRadius
                        )
                        +
                        (
                            -_direction.x * 2
                        )
                        ,
                        (
                            transform.position.y + spawnCircleRadius
                        )
                        +
                        (
                            -_direction.x * 2
                        )
                    )
                )
            );

        _spore = Instantiate(sporeInstance, transform.position, quaternion.identity).GetComponent<SporeRootInstance>();
        
        _spore.SetGoal(transform.position,sporeRootGoalPosition);
        
        ResetCastTimer();
    }

    private void ResetCastTimer()
        => _timeBtwCast = maximumTimeBtwCast;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + -_direction, spawnCircleRadius);
    }
}
