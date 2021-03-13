using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HorizontalMoving : BatchUpdateObject
{
    [Header("Reference")]
    [SerializeField]
    private FloatVariable _currentSpeed;

    [Header("Unity Event")]
    [SerializeField]
    private UnityEvent _onPrepareForNewGame;

    [Header("Events in")]
    [SerializeField]
    private GameEvent _onGameEnd;
    [SerializeField]
    private GameEvent _onGameRestart;

    [Header("Config")]
    [SerializeField]
    private float _moveSpeedFactor = 1f;
    
    public bool Moving { get; set; }

    public override void OnUpdate()
    {
        if (!Moving)
        {
            return;
        }

        transform.position += Vector3.left * _currentSpeed.Value * _moveSpeedFactor * Time.deltaTime;
    }

    private void StopMove(object[] args)
    {
        Moving = false;
    }

    private void PrepareForNewGame(object[] args)
    {
        _onPrepareForNewGame.Invoke();
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
