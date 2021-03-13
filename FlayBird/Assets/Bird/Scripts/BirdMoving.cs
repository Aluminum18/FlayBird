using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BirdMoving : BatchUpdateObject
{
    [Header("Config")]
    [SerializeField]
    private Vector3 _startPos;
    [SerializeField]
    private float _flapForce;
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private bool _underGravity;

    [Header("Events in")]
    [SerializeField]
    private GameEvent _onBirdFlap;
    [SerializeField]
    private GameEvent _onPreGame;
    [SerializeField]
    private GameEvent _onGameStart;

    [Header("Unity Events")]
    [SerializeField]
    private UnityEvent _onFlapped;
    [SerializeField]
    private UnityEvent _onMovedToStartPoint;

    private float _currentVelocity = 0f;
    private bool _flappable;
    public bool Flappable
    {
        set
        {
            _flappable = value;
        }
    }

    private void Flap(object[] args)
    {
        if (_flappable == false)
        {
            return;
        }

        _currentVelocity = _flapForce;
        _onFlapped.Invoke();
    }

    private void Prepare(params object[] args)
    {
        _underGravity = false;
        _flappable = true;

        transform.position = _startPos;
        _currentVelocity = 0f;

        _onMovedToStartPoint.Invoke();
    }

    private void ApplyGravity(params object[] args)
    {
        _underGravity = true;
        _currentVelocity -= _gravity * Time.deltaTime;
    }

    public override void OnUpdate()
    {
        if (_underGravity)
        {
            ApplyGravity();
        }

        transform.position += Vector3.up * _currentVelocity * Time.deltaTime;
    }

    private void OnEnable()
    {
        _onBirdFlap.Subcribe(Flap);

        _onPreGame.Subcribe(Prepare);
        _onGameStart.Subcribe(ApplyGravity);
    }

    private void OnDisable()
    {
        _onBirdFlap.Unsubcribe(Flap);

        _onPreGame.Unsubcribe(Prepare);
        _onGameStart.Unsubcribe(ApplyGravity);
    }
}
