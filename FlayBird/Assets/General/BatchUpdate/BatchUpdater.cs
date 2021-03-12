using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatchUpdater : MonoSingleton<BatchUpdater>
{
    private List<BatchUpdateObject> _objects = new List<BatchUpdateObject>();

    public void RegisterUpdate(BatchUpdateObject obj)
    {
        _objects.Add(obj);
    }

    public void UnregisterUpdate(BatchUpdateObject obj)
    {
        _objects.Remove(obj);
    }

    private void Update()
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            var obj = _objects[i];
            if (!obj.isActiveAndEnabled)
            {
                continue;
            }

            obj.OnUpdate();
        }
    }
}
