using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatchUpdateObject : MonoBehaviour
{
    public bool Active { get; set; }

    public virtual void OnUpdate()
    {
        if (!Active)
        {
            return;
        }
    }

    private void Awake()
    {
        BatchUpdater.Instance.RegisterUpdate(this);
        Active = true;
        OnAwake();
    }

    private void OnDestroy()
    {
        BatchUpdater.Instance.UnregisterUpdate(this);
        DoOnDestroy();
    }

    public virtual void OnAwake()
    {
    }

    public virtual void DoOnDestroy()
    {
    }
}
