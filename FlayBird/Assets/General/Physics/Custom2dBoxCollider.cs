using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom2dBoxCollider : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private float _width;
    [SerializeField]
    private float _height;

    [Header("Inspec")]
    [SerializeField]
    private float _xMin;
    [SerializeField]
    private float _xMax;
    [SerializeField]
    private float _yMin;
    [SerializeField]
    private float _yMax;
    [SerializeField]
    private HashSet<int> _intersectings = new HashSet<int>();

    public float XMin
    {
        get
        {
            return transform.position.x - _width / 2f;
        }
    }

    public float XMax
    {
        get
        {
            return transform.position.x + _width / 2f;
        }
    }

    public float YMin
    {
        get
        {
            return transform.position.y - _height / 2f;
        }
    }

    public float YMax
    {
        get
        {
            return transform.position.y + _height / 2f;
        }
    }

    private CustomPhysics _customPhysics;

    private void OnDrawGizmosSelected()
    {
        _xMin = XMin;
        _xMax = XMax;
        _yMin = YMin;
        _yMax = YMax;

        Vector3 A = new Vector3(_xMin, _yMin, 0f);
        Vector3 B = new Vector3(_xMin, _yMax, 0f);
        Vector3 C = new Vector3(_xMax, _yMax, 0f);
        Vector3 D = new Vector3(_xMax, _yMin, 0f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(A, B);
        Gizmos.DrawLine(B, C);
        Gizmos.DrawLine(C, D);
        Gizmos.DrawLine(A, D);
    }

    public bool IsInCurrentIntersectings(Custom2dBoxCollider other)
    {
        return _intersectings.Contains(other.gameObject.GetInstanceID());
    }

    public void IntersectInWith(Custom2dBoxCollider other)
    {
        int otherId = other.gameObject.GetInstanceID();
        if (_intersectings.Contains(otherId))
        {
            return;
        }

        _intersectings.Add(otherId);
        SendMessage("OnCollideInWith", other);
    }

    public void IntersectOutWith(Custom2dBoxCollider other)
    {
        _intersectings.Remove(other.gameObject.GetInstanceID());
        SendMessage("OnCollideOutWith", other);
    }

    private void Awake()
    {
        _customPhysics = CustomPhysics.Instance;
        _customPhysics.RegisterPhysicsUpdate(this);
    }

    private void OnDestroy()
    {
        if (_customPhysics == null)
        {
            return;
        }

        _customPhysics.UnregisterPhysicUpdate(this);
    }
}
