using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : InitBase
{
    public GameObject HeroRespawn { get; private set; }
    public GameObject EnemyRespawn { get; private set; }
    public GameObject TargetBuilding { get; private set; }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Map.LoadMap("TestMap");

        HeroRespawn = GameObject.Find(Define.HeroRespawn);
        EnemyRespawn = GameObject.Find(Define.EnemyRespawn);
        TargetBuilding = GameObject.Find(Define.TargetBuilding);

        #region Objects Spawn and Register on ObjectManager
        Building targetBuilding = TargetBuilding.GetComponent<Building>();
        Managers.Obj.Register(targetBuilding);

        Hero hero = Managers.Obj.Spawn<Hero>(HeroRespawn.transform.position);
        Managers.Map.MoveTo(hero, Managers.Map.World2Cell(hero.transform.position), hero.CreatureType ,true);

        Enemy enemy = Managers.Obj.Spawn<Enemy>(EnemyRespawn.transform.position);
        Managers.Map.MoveTo(enemy, Managers.Map.World2Cell(enemy.transform.position),enemy.CreatureType ,true);
        #endregion

        return true;
    }
}
