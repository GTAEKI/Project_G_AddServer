using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseMapManager
{
    public List<SavedObject> SavedBuildingObjects { get; private set; }
    public GridData SavedOccupiedData { get; private set; }
    public BaseMapManager()
    {
        SavedBuildingObjects = new();
        SavedOccupiedData = new();
    }


    public void AddBuildingToList(SavedObject savedObject)
    {
        SavedBuildingObjects.Add(savedObject);        
    }

    public void AddToOccupiedList(GridData gridData)
    {        
        SavedOccupiedData = gridData;
    }

    public int GetListLength()
    {
        int count = SavedBuildingObjects.Count;
        return count;
    }

    public void SavedDataClear()
    {
        SavedBuildingObjects.Clear();
        SavedOccupiedData.Clear();
    }


    public List<int> GetSavedObejctID()
    {
        List<int> ids = new();
        for (int i = 0; i < SavedBuildingObjects.Count; i++)
        {
            ids.Add(SavedBuildingObjects[i].ID);
        }
        return ids;
    }
    public class SavedObject
    {
        public int ID { get; private set; }
        public Vector3Int GridPos { get; private set; }
        public GameObject Prefab { get; private set; }        
        public SavedObject(int ID, Vector3Int pos, GameObject prefab)
        {
            this.ID = ID;
            this.GridPos = pos;
            this.Prefab = prefab;            
        }
    }
}
