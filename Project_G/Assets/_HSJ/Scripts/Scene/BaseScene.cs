using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BaseMapManager;

public class BaseScene : InitBase
{
    [SerializeField]
    private ObjectPlacer objectPlacer;
    [SerializeField]
    Grid grid;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Sound.Play(Define.ESound.Bgm, "BaseScene");
        int length = Managers.BaseMap.GetListLength();
        for (int i = 0; i < length; i++)
        {
            SavedObject sbo = Managers.BaseMap.SavedBuildingObjects[i];
            objectPlacer.PlaceSavedObject(sbo.Prefab, grid.CellToWorld(sbo.GridPos));
        }
        return true;
    }
}
