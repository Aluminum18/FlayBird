using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : BatchUpdateObject
{
    [Header("Config")]
    [SerializeField]
    private float _flapForce;
    [SerializeField]
    private float _gravity;

    [Header("Events in")]
    [SerializeField]
    private GameEvent _onBirdFlap;

    private float _currentVelocity = 0f;

    private void Flap(object[] args)
    {
        _currentVelocity = _flapForce;
    }

    private void ApplyGravity()
    {
        _currentVelocity -= _gravity * Time.deltaTime;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        ApplyGravity();

        transform.position += Vector3.up * _currentVelocity * Time.deltaTime;
    }



    private void OnEnable()
    {
        _onBirdFlap.Subcribe(Flap);
    }

    private void OnDisable()
    {
        _onBirdFlap.Unsubcribe(Flap);
    }
}
