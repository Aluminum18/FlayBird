using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BirdMoving : BatchUpdateObject
{
    [Header("Config")]
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
    private GameEvent _onGameStart;

    [Header("Unity Events")]
    [SerializeField]
    private UnityEvent _onFlapped;

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
        _flappable = true;
        _onBirdFlap.Subcribe(Flap);
        _onGameStart.Subcribe(ApplyGravity);
    }

    private void OnDisable()
    {
        _onBirdFlap.Unsubcribe(Flap);
        _onGameStart.Unsubcribe(ApplyGravity);
    }
}
