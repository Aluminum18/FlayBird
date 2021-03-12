using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BirdContact : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onBirdHit;

    public void OnCollideInWith(Custom2dBoxCollider other)
    {
        Debug.Log("BirdTrigger in");
        _onBirdHit.Invoke();
    }

    public void OnCollideOutWith(Custom2dBoxCollider other)
    {
        Debug.Log("BirdTrigger out");
    }
}
