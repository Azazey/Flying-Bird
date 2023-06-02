using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Pool<T> where T : MonoBehaviour
{
    private T _prefab;
    private bool _autoExpand;
    private Transform _container;

    public List<T> PoolList;

    public Pool(T prefab, int count, bool autoExpand)
    {
        _prefab = prefab;
        _container = null;
        _autoExpand = autoExpand;
        CreatePool(count);
    }

    public Pool(T prefab, int count, bool autoExpand, Transform container)
    {
        _prefab = prefab;
        _container = container;
        _autoExpand = autoExpand;
        CreatePool(count);
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
            return element;

        if (_autoExpand)
            return CreateObject(true);

        throw new Exception(
            $"There is no free element, try to turn \"on\" AutoExpand or redo all you fricking script, btw this element is making issue:{typeof(T)}");
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var mono in PoolList)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    private void CreatePool(int count)
    {
        PoolList = new List<T>();

        for (int i = 0; i < count; i++)
            CreateObject();
    }

    private T CreateObject(bool isActivatedByDefault = false)
    {
        var createdObject = Object.Instantiate(_prefab, _container);
        createdObject.gameObject.SetActive(isActivatedByDefault);
        createdObject.transform.parent = null;
        PoolList.Add(createdObject);
        return createdObject;
    }
}
