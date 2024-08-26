using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    Dictionary<string, UI_Base> UIs = new Dictionary<string, UI_Base>();

    public void Register<T>(T ui) where T : UI_Base
    {
        string key = typeof(T).Name;
        if (UIs.ContainsKey(key) == false)
            UIs.Add(key, ui);
    }

    public T Get<T>() where T : UI_Base
    {
        return UIs[typeof(T).Name] as T;
    }

    public void Remove<T>()
    {
        UIs.Remove(typeof(T).Name);
    }
}
