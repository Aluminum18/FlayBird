using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BirdContact : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private LayerMask _hitBy;
    [SerializeField]
    private LayerMask _scoredBy;

    [SerializeField]
    private UnityEvent _onBirdScored;
    [SerializeField]
    private UnityEvent _onBirdHit;

    public void OnCollideInWith(Custom2dBoxCollider other)
    {
        if ((1 << other.gameObject.layer & _hitBy) == 0)
        {
            return;
        }

        _onBirdHit.Invoke();
    }

    public void OnCollideOutWith(Custom2dBoxCollider other)
    {
        if ((1 << other.gameObject.layer & _scoredBy) == 0)
        {
            return;
        }

        _onBirdScored.Invoke();
    }
}
