using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

internal class Pool 
{
    private GameObject _prefab;
    private IObjectPool<GameObject> _pool;

    private Transform _root;
    private Transform Root 
    {
        get 
        {
            if (_root == null) 
            {
                GameObject go = new GameObject() { name = $"@{_prefab.name}Pool" };
                _root = go.transform;
            }

            return _root;
        }
    }

    public Pool(GameObject go) 
    {
        _prefab = go;
        _pool = new ObjectPool<GameObject>(Create, OnGet, OnRelease, OnDestroy);
    }

    private GameObject Create() 
    {
        GameObject go = GameObject.Instantiate(_prefab);
        go.transform.SetParent(Root);
        go.name = _prefab.name;
        return go;
    }
    private void OnGet(GameObject go) 
    {
        go.SetActive(true);
    }
    private void OnRelease(GameObject go)
    {
        go.SetActive(false);
    }
    private void OnDestroy(GameObject go)
    {
        GameObject.Destroy(go);
    }

    public void Push(GameObject go) 
    {
        if(go.activeSelf)
            _pool.Release(go);
    }

    public GameObject Pop() 
    {
        return _pool.Get();
    } 
}

public class PoolManager
{
    private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

    public void Create(GameObject go) 
    {
        Pool pool = new Pool(go);
        _pools.Add(go.name, pool);
    }

    public bool Push(GameObject go) 
    {
        if (_pools.ContainsKey(go.name) == false)
            return false;

        _pools[go.name].Push(go);
        return true;
    }

    public GameObject Pop(GameObject go) 
    {
        if (_pools.ContainsKey(go.name) == false)
            Create(go);

        GameObject obj = _pools[go.name].Pop();
        return obj;
    }

    public void Clear() 
    {
        _pools.Clear();
    }
}
