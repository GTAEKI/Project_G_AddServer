using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    private List<GameObject> placedGameObject = new List<GameObject>();

    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = position;
        placedGameObject.Add(newObject);
        return placedGameObject.Count - 1;
    }

    public void PlaceSavedObject(GameObject prefab, Vector3 position)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = position;    
    }
    
}
