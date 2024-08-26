using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Util
{
    public static void LoadScene(Define.EScene sceneType) 
    {
        SceneManager.LoadScene(sceneType.ToString());
    }

	public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
	{
		Transform transform = FindChild<Transform>(go, name, recursive);
		if (transform == null)
			return null;

		return transform.gameObject;
	}

	public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
	{
		if (go == null)
			return null;

		if (recursive == false)
		{
			for (int i = 0; i < go.transform.childCount; i++)
			{
				Transform transform = go.transform.GetChild(i);
				if (string.IsNullOrEmpty(name) || transform.name == name)
				{
					T component = transform.GetComponent<T>();
					if (component != null)
						return component;
				}
			}
		}
		else
		{
			foreach (T component in go.GetComponentsInChildren<T>())
			{
				if (string.IsNullOrEmpty(name) || component.name == name)
					return component;
			}
		}

		return null;
	}

    public static Transform GetRootTransform(string name)
    {
        GameObject root = GameObject.Find(name);
        if (root == null)
            root = new GameObject { name = name };

        return root.transform;
    }

    // HSJ 
    public static GameObject GetRootObject(string objectName)
    {
        Scene scene = SceneManager.GetActiveScene();
        GameObject[] rootObjects = scene.GetRootGameObjects();
        GameObject targetObject = null;        
        foreach (GameObject go in rootObjects)
        {
            if (go.name.Equals(objectName))
            {
                targetObject = go;
                return targetObject;
            }
            else
            {
                Debug.LogError("Can't find " + objectName + "rootObject");
                targetObject = new GameObject("objectName");                
            }
        }
        return targetObject;
    }
    
}
