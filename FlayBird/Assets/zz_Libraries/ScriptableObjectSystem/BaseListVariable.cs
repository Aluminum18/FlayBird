using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseListVariable<T> : ScriptableObject
{
    [SerializeField]
    protected List<T> _list = new List<T>();
    [SerializeField]
    protected bool _dontDuplicateValue = false;

    public T LastAdd { get; private set; }
    public T LastRemove { get; private set; }
    public int LastChangedIndex { get; private set; }

    public delegate void OnListCountChangedDel();
    public event OnListCountChangedDel OnListChanged;

    public List<T> List
    {
        get
        {
            return _list;
        }
    }

    public void AssignNew(List<T> newList)
    {
        if (newList == null)
        {
            return;
        }

        _list.Clear();

        for (int i = 0; i < newList.Count; i++)
        {
            _list.Add(newList[i]);
        }

        OnListChanged?.Invoke();
    }

    public void CompareWithNewList(List<T> newList)
    {
        if (_list.Count != newList.Count)
        {
            OnListChanged?.Invoke();
        }

        for (int i = 0; i < _list.Count; i++)
        {
            if (!Compare(_list[i], newList[i]))
            {
                OnListChanged?.Invoke();
                break;
            }
        }
    }

    public void Add(T item)
    {
        if (_dontDuplicateValue)
        {
            if (_list.Contains(item))
            {
                return;
            }
        }

        _list.Add(item);

        LastAdd = item;

        OnListChanged?.Invoke();
    }

    public void Remove(T item)
    {
        int preCount = _list.Count;
        _list.Remove(item);

        if (preCount == _list.Count)
        {
            Debug.Log($"value [{item}] is not contained in list, ignore!");
            return;
        }

        LastRemove = item;

        OnListChanged?.Invoke();
    }

    public void UpdateValueAt(int idx, T newValue)
    {
        if (Compare(_list[idx], newValue))
        {
            return;
        }

        _list[idx] = newValue;

        LastChangedIndex = idx;

        OnListChanged?.Invoke();
    }

    public bool Contain(T item)
    {
        return _list.Contains(item);
    }

    public void Clear()
    {
        _list.Clear();
        OnListChanged?.Invoke();
    }

    protected virtual bool Compare(T item1, T item2)
    {
        return false;
    }

    private void OnDisable()
    {
        _list.Clear();
    }
}
