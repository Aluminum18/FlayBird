using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInPool : MonoBehaviour
{
    private ObjectPool _container;
    public ObjectPool Container
    {
        set
        {
            _container = value;
        }
    }

    private void OnDisable()
    {
        _container.ReturnToPool(gameObject);
    }
}
