
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> _pool;

    public T prefab { get; }
    public bool autoExpand { get; set; }
    public Transform container { get; }

    public ObjectPool(T prefab, int count)
    {
        this.prefab = prefab;
        container = null;

        CreatePool(count);
    }

    public ObjectPool(T prefab, int count, Transform container)
    {
        this.prefab = prefab;
        this.container = container;

        CreatePool(count);
    }

    public void CreatePool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
            CreateObject();
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createObject = UnityEngine.Object.Instantiate(prefab, container);
        createObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(createObject);
        return createObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach(var item in _pool)
        {
            if(!item.gameObject.activeInHierarchy)
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if(HasFreeElement(out var element))
            return element;

        if (autoExpand)
            return CreateObject(true);

        throw new Exception($"There is no free elements in pooloftype {typeof(T)}");
    }
}
