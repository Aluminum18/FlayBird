using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMoving : BatchUpdateObject
{
    [Header("Reference")]
    [SerializeField]
    private FloatVariable _currentSpeed;

    [Header("Events in")]
    [SerializeField]
    private GameEvent _onGameEnd;
    [SerializeField]
    private GameEvent _onGameRestart;
    
    public bool Moving { get; set; }

    public override void OnUpdate()
    {
        if (!Moving)
        {
            return;
        }

        transform.position += Vector3.left * _currentSpeed.Value * Time.deltaTime;
    }

    private void StopMove(object[] args)
    {
        Moving = false;
    }

    private void PrepareForNewGame(object[] args)
    {
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        Moving = true;
        _onGameEnd.Subcribe(StopMove);
        _onGameRestart.Subcribe(PrepareForNewGame);
    }

    private void OnDisable()
    {
        _onGameEnd.Unsubcribe(StopMove);
        _onGameRestart.Unsubcribe(PrepareForNewGame);
    }
}
