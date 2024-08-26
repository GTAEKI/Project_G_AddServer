using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResourceManager
{
    // Resources 폴더에서 읽어오기
    public T LoadFromResources<T>(string key) where T : Object
    {
        return Resources.Load<T>($"{key}");
    }

    public GameObject Instantiate(string key, bool pooling = false)
    {
        GameObject obj = LoadFromResources<GameObject>(key);

        if (pooling) 
        {
            return Managers.Pool.Pop(obj);
        }

        return Object.Instantiate(obj);
    }

    public void Destroy(GameObject go, bool pooling)
    {
        if (go == null)
            return;

        if (Managers.Pool.Push(go))
            return;

        Object.Destroy(go);
    }
}
