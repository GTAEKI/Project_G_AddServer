using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new Dictionary<Vector3Int, PlacementData>();

    public void AddObject(Vector3Int gridPosition, Vector2Int objectsSize, int ID, int placedObjectIndex, Define.EPlacementBuildingType type)
    {
        List<Vector3Int> positionToOccupy = CalculatePosition(gridPosition, objectsSize);
        PlacementData data = new PlacementData(positionToOccupy, ID, placedObjectIndex, type);
        foreach(var pos in positionToOccupy)
        {
            if(placedObjects.ContainsKey(pos))
            {
                throw new Exception($"Dictionary already contains this cell position {pos}");
            }
            placedObjects[pos] = data;
        }
    }


    private List<Vector3Int> CalculatePosition(Vector3Int gridPosition, Vector2Int objectsSize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = 0; x < objectsSize.x; x++)
        {
            for (int y = 0; y < objectsSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }
        return returnVal;
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectsSize)
    {
        List<Vector3Int> positionToOccupy = CalculatePosition(gridPosition, objectsSize);
        foreach(var pos in positionToOccupy)
        {
            if(placedObjects.ContainsKey(pos))
            {
                return false;
            }
        }
        return true;
    }

    public void Clear()
    {
        placedObjects.Clear();
    }


}

public class PlacementData
{
    public List<Vector3Int> occupiedPositions;
    public int ID { get; private set; }
    public int PlacedObjectIndex { get; private set; }
    public Define.EPlacementBuildingType Type { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPositions, int ID, int placedObjectIndex, Define.EPlacementBuildingType type)
    {
        this.occupiedPositions = occupiedPositions;
        this.ID = ID;
        PlacedObjectIndex = placedObjectIndex;
        Type = type;
    }
}

