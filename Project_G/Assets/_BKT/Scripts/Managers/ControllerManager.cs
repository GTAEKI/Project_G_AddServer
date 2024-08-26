using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager
{
    private Dictionary<string, Object> controllers = new Dictionary<string, Object>();

    public void Register<T>(T controller) where T : Object, IController
    {
        string name = typeof(T).Name;
        controllers[name] = controller;
    }

    public T Get<T>() where T : Object, IController 
    {
        string name = typeof(T).Name;
        if (controllers.ContainsKey(name) == true) 
        {
            return controllers[name] as T;
        }

        return null;
    }

    public void Clear() 
    {
        controllers.Clear();
    }
}
