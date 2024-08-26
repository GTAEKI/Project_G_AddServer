using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{

    [SerializeField]
    private GameInput gameInput;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectDatabaseSO database;

    [SerializeField]
    private GameObject gridVisualization;

    private GridData floorData;
    private GridData buildingData;

    [SerializeField]
    private PreviewSystem preview;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    [SerializeField]
    private ObjectPlacer objectPlacer;

    IBuildingState buildingState;

    void Start()
    {
        gridVisualization.SetActive(false);
        floorData = new GridData();
        buildingData = new GridData();
    }
    public void StartPlacement(int ID)
    {
        StopPlacement();
        gridVisualization.SetActive(true);

        buildingState = new PlacementState(ID,
                                           grid,
                                           preview,
                                           database,
                                           floorData,
                                           buildingData,
                                           objectPlacer);

        gameInput.OnClicked += PlaceStructure;
        gameInput.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (gameInput.IsPointerOverUI())
        {
            return;
        }

        Vector3 mousePosition = gameInput.GetMousePosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        buildingState.OnAction(gridPosition);
    }


    private void StopPlacement()
    {
        if(buildingState == null) { return; }

        gridVisualization.SetActive(false);
        
        buildingState.EndState();

        gameInput.OnClicked -= PlaceStructure;
        gameInput.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;

        buildingState = null;
    }

    void Update()
    {
        if(buildingState  == null) { return; }
        

        Vector3 mousePosition = gameInput.GetMousePosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        if(lastDetectedPosition != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDetectedPosition = gridPosition;
        }
    }
    
}
