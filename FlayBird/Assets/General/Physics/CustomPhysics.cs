using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPhysics : MonoSingleton<CustomPhysics>
{
    [Header("Config")]
    [Tooltip("Only check 'CheckedCollider' to others")]
    [SerializeField]
    private bool _privateCheck;
    [SerializeField]
    private Custom2dBoxCollider _checkedCollider;

    [Header("Inspec")]
    [SerializeField]
    private List<Custom2dBoxCollider> _colliders = new List<Custom2dBoxCollider>();

    public void RegisterPhysicsUpdate(Custom2dBoxCollider collider)
    {
        _colliders.Add(collider);
    }

    public void UnregisterPhysicUpdate(Custom2dBoxCollider collider)
    {
        _colliders.Remove(collider);
    }

    private void FixedUpdate()
    {
        if (_privateCheck && _checkedCollider != null)
        {
            Check1ToOthers(_checkedCollider);
            return;
        }

        FullCheck();
    }

    private void FullCheck()
    {
        // not need yet for this excercise :))
    }

    private void Check1ToOthers(Custom2dBoxCollider checkedCollider)
    {
        for (int i = 0; i < _colliders.Count; i++)
        {
            var other = _colliders[i];

            if (_checkedCollider.gameObject.GetInstanceID() == other.gameObject.GetInstanceID())
            {
                continue;
            }

            if ((_checkedCollider.XMin <= other.XMax && _checkedCollider.XMax >= other.XMin) &&
                (_checkedCollider.YMin <= other.YMax && _checkedCollider.YMax >= other.YMin))
            {
                _checkedCollider.IntersectInWith(other);
                continue;
            }

            if (_checkedCollider.IsInCurrentIntersectings(other))
            {
                _checkedCollider.IntersectOutWith(other);
            }
        }
    }
}
