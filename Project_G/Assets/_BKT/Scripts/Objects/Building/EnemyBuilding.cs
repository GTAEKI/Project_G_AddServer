using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EnemyBuilding : Building
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BuildingType = Define.EBuildingType.EnemyBuilding;

        return true;
    }

    public void CreateEnemy() 
    {
        EColorType randColor = (EColorType)Random.Range(1,4);
        //EColorType randColor = EColorType.Red;
        Enemy enemy = null;

        switch (randColor) 
        {
            case EColorType.White:
                enemy = Managers.Obj.Spawn<Enemy_White>(transform.position,true);
                break;
            case EColorType.Red:
                enemy = Managers.Obj.Spawn<Enemy_Red>(transform.position,true);
                break;
            case EColorType.Yellow:
                enemy = Managers.Obj.Spawn<Enemy_Yellow>(transform.position, true);
                break;
        }

        Managers.Map.MoveTo(enemy, Managers.Map.World2Cell(enemy.transform.position),enemy.CreatureType,true);
    }
}
