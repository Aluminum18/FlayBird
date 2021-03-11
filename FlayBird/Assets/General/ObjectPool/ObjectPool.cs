using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectInPool;
    [SerializeField]
    private Transform _spawnPos;

    private Stack<GameObject> _availables = new Stack<GameObject>();

    private void OnValidate()
    {
        if (_objectInPool == null)
        {
            Debug.LogWarning($"Invalid config in [{gameObject.name}]", this);
        }
    }

    public GameObject GetObject()
    {
        GameObject go;
        if (_availables.Count == 0)
        {
            go = Instantiate(_objectInPool, _spawnPos ? _spawnPos.position : Vector3.zero, Quaternion.identity);
            go.AddComponent<ObjectInPool>().Container = this;
            return go;
        }

        go = _availables.Pop();

        if (go.activeSelf)
        {
            go = GetObject();
        }

        go.SetActive(true);

        go.transform.position = Vector3.zero;
        if (_spawnPos != null)
        {
            go.transform.position = _spawnPos.position;
        }

        return go;
    }

    public void ReturnToPool(GameObject go)
    {
        go.SetActive(false);
        _availables.Push(go);
    }
}
