using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMoving : BatchUpdateObject
{
    [Header("Reference")]
    [SerializeField]
    private FloatVariable _currentObstacleSpeed;

    [Header("Events in")]
    [SerializeField]
    private GameEvent _onGameEnd;
    
    public bool Moving { get; set; }

    public override void OnUpdate()
    {
        if (!Moving)
        {
            return;
        }

        transform.position += Vector3.left * _currentObstacleSpeed.Value * Time.deltaTime;
    }

    private void StopMove(object[] args)
    {
        Moving = false;
    }

    public override void OnAwake()
    {
        _onGameEnd.Subcribe(StopMove);
    }

    private void OnDestroy()
    {
        _onGameEnd.Unsubcribe(StopMove);
    }
}
