using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatchUpdateObject : MonoBehaviour
{
    private BatchUpdater _batchUpdater;
    public virtual void OnUpdate()
    {

    }

    private void Awake()
    {
        _batchUpdater = BatchUpdater.Instance;
        _batchUpdater.RegisterUpdate(this);

        OnAwake();
    }

    private void OnDestroy()
    {
        if (_batchUpdater != null)
        {
            _batchUpdater.UnregisterUpdate(this);
        }

        DoOnDestroy();
    }

    public virtual void OnAwake()
    {
    }

    public virtual void DoOnDestroy()
    {
    }
}
