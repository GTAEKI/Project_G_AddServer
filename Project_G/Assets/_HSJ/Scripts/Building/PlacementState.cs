using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BaseMapManager;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    ObjectDatabaseSO database;
    GridData floorData;
    GridData buildingData;
    ObjectPlacer objectPlacer;
    

    public PlacementState(int iD,
                          Grid grid,
                          PreviewSystem previewSystem,
                          ObjectDatabaseSO database,
                          GridData floorData,
                          GridData buildingData,
                          ObjectPlacer objectPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.floorData = floorData;
        this.buildingData = buildingData;
        this.objectPlacer = objectPlacer;

        selectedObjectIndex = database.objectData.FindIndex(data => data.ID == ID);
        // not selected
        if (selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(database.objectData[selectedObjectIndex].Prefab,
                database.objectData[selectedObjectIndex].Size);
        }
        else
        {
            throw new System.Exception($"No Object with ID {iD}");
        }
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        bool placemetValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        bool priceValidity = CheckPriceValidity();
        bool limitValidity = CheckPlacementLimit(selectedObjectIndex);
        if(placemetValidity == false || priceValidity == false || limitValidity == false)
        {
            Managers.Sound.Play(Define.ESound.Effect, $"Error1");
            return;
        }


        int index 
            = objectPlacer.PlaceObject(database.objectData[selectedObjectIndex].Prefab, grid.CellToWorld(gridPosition));

        GridData selectedData = database.objectData[selectedObjectIndex].BuildingType == 0 ? floorData : Managers.BaseMap.SavedOccupiedData;
        selectedData.AddObject(gridPosition,
            database.objectData[selectedObjectIndex].Size,
            database.objectData[selectedObjectIndex].ID,
            index,
            database.objectData[selectedObjectIndex].BuildingType);
        
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);

        SavedObject sbo = new SavedObject(database.objectData[selectedObjectIndex].ID,
            gridPosition,
            database.objectData[selectedObjectIndex].Prefab);


        if (selectedData == buildingData) { Managers.BaseMap.AddToOccupiedList(selectedData); }  
        Managers.BaseMap.AddBuildingToList(sbo);
        Managers.Scrap.RemoveScrap(database.objectData[selectedObjectIndex].BuildingPrice);
        Managers.Quest.UpdateQuestState(ID);
        Managers.Quest.CheckAllQuestClear();
        Managers.Sound.Play(Define.ESound.Effect, $"Placement");
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = database.objectData[selectedObjectIndex].BuildingType == 0 ? floorData : Managers.BaseMap.SavedOccupiedData;
        //GridData selectedData = database.objectData[selectedObjectIndex].BuildingType == 0 ? floorData : buildingData;
        
        return selectedData.CanPlaceObjectAt(gridPosition, database.objectData[selectedObjectIndex].Size);
    }

    private bool CheckPriceValidity()
    {
        bool isAffordable = database.objectData[selectedObjectIndex].BuildingPrice <= Managers.Scrap.Scrap ? true: false;
        return isAffordable;
    }

    private bool CheckPlacementLimit(int selectedObjectID)
    {
        bool isPlaceable = Managers.Quest.IsQuestClear(selectedObjectID);

        return isPlaceable;           
    }

    private bool CheckPlacemetnOut(Vector3Int gridPosition, int selectedObjectIndex)
    {


        return false;
    }


    public void UpdateState(Vector3Int gridPosition)
    {
        bool placemetValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placemetValidity);
    }

}
