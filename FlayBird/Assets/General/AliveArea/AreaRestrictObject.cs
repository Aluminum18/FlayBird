using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaRestrictObject : BatchUpdateObject
{
    [Header("Config")]
    [SerializeField]
    private Vector2 _restrictAreaCenter;
    [SerializeField]
    private float _width;
    [SerializeField]
    private float _height;
    [SerializeField]
    private bool _verticalRestrict;
    [SerializeField]
    private bool _horizonRestrict;
    [SerializeField]
    private OutOfAreaAction _outOfAreaAction;

    [Header("Events out")]
    [SerializeField]
    private GameEvent _onOutOfRestrictArea;

    private float _xMin;
    private float _xMax;
    private float _yMin;
    private float _yMax;

    private void OnDrawGizmosSelected()
    {
        CalculateBoundary();

        Vector3 A = new Vector3(_xMin, _yMin, 0f);
        Vector3 B = new Vector3(_xMin, _yMax, 0f);
        Vector3 C = new Vector3(_xMax, _yMax, 0f);
        Vector3 D = new Vector3(_xMax, _yMin, 0f);

        Gizmos.color = Color.green;
        if (_verticalRestrict)
        {
            Gizmos.DrawLine(A, D);
            Gizmos.DrawLine(B, C);
        }

        if (_horizonRestrict)
        {
            Gizmos.DrawLine(A, B);
            Gizmos.DrawLine(C, D);
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_horizonRestrict)
        {
            if (CheckHorizon())
            {
                return;
            }     
        }

        if (_verticalRestrict)
        {
            CheckVertical();
        }
    }

    public override void OnAwake()
    {
        base.OnAwake();

        CalculateBoundary();
    }

    private void CalculateBoundary()
    {
        _xMin = _restrictAreaCenter.x - _width / 2f;
        _xMax = _restrictAreaCenter.x + _width / 2f;

        _yMin = _restrictAreaCenter.y - _height / 2f;
        _yMax = _restrictAreaCenter.y + _height / 2f;
    }

    private bool CheckHorizon()
    {
        bool isOut = transform.position.x < _xMin || transform.position.x > _xMax;

        if (isOut)
        {
            OutOfArea();
        }

        return isOut;
    }

    private bool CheckVertical()
    {
        bool isOut = transform.position.y < _yMin || transform.position.y > _yMax;

        if (isOut)
        {
            OutOfArea();
        }

        return isOut;
    }

    private void OutOfArea()
    {
        switch (_outOfAreaAction)
        {
            case OutOfAreaAction.None:
                break;
            case OutOfAreaAction.Disable:
                gameObject.SetActive(false);
                break;
            case OutOfAreaAction.Event:
                {
                    _onOutOfRestrictArea?.Raise();
                    break;
                }
            case OutOfAreaAction.DisableAndEvent:
                {
                    gameObject.SetActive(false);
                    _onOutOfRestrictArea?.Raise();
                    break;
                }
        }
    }
}

public enum OutOfAreaAction
{
    None = 0,
    Disable,
    Event,
    DisableAndEvent
}
