using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDefaultTrasform : MonoBehaviour
{
    [SerializeField]
    private Vector3 _defaultPosition;

    public void ToDefaultPosition()
    {
        transform.position = _defaultPosition;
    }
}
