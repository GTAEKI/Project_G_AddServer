using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager
{
    public HashSet<Hero> Heroes { get; } = new HashSet<Hero>();
    public HashSet<Enemy> Enemies { get; } = new HashSet<Enemy>();
    public HashSet<TargetBuilding> TargetBuildings { get; } = new HashSet<TargetBuilding>();
    public HashSet<EnemyBuilding> EnemyBuildings { get; } = new HashSet<EnemyBuilding> { };
    public HashSet<HeroSpawnArea> HeroSpawnAreas { get; } = new HashSet<HeroSpawnArea> { };

    #region Make Root
    public Transform HeroRoot { get { return Util.GetRootTransform("@Heroes"); } }
    public Transform EnemyRoot { get { return Util.GetRootTransform("@Enemies"); } }
    public Transform TargetBuildingRoot { get { return Util.GetRootTransform("@TargetBuildingRoot"); }  }
    public Transform EnemyBuildingRoot { get { return Util.GetRootTransform("@EnemyBuildingRoot"); } }
    public Transform HeroSpawnPointRoot { get { return Util.GetRootTransform("@HeroSpawnPointRoot"); } }
    #endregion

    // 등록
    public bool Register<T>(T obj) where T : BaseObject
    {
        if (obj.ObjectType == Define.EObjectType.Creature)
        {
            Creature creature = obj as Creature;
            switch (creature.CreatureType)
            {
                case Define.ECreatureType.Hero:
                    obj.transform.parent = HeroRoot;
                    Hero hero = creature as Hero;
                    Heroes.Add(hero);
                    break;
                case Define.ECreatureType.Enemy:
                    //obj.transform.parent = EnemyRoot;
                    Enemy enemy = creature as Enemy;
                    Enemies.Add(enemy);
                    break;
                default:
                    return false;
            }
            creature.SetInfo();
        }
        else if (obj.ObjectType == Define.EObjectType.Building)
        {
            Building building = obj as Building;
            switch (building.BuildingType)
            {
                case Define.EBuildingType.TargetBuilding:
                    obj.transform.parent = TargetBuildingRoot;
                    TargetBuilding targetBuilding = building as TargetBuilding;
                    TargetBuildings.Add(targetBuilding);
                    break;
                case Define.EBuildingType.EnemyBuilding:
                    obj.transform.parent = EnemyBuildingRoot;
                    EnemyBuilding enemyBuilding = building as EnemyBuilding;
                    EnemyBuildings.Add(enemyBuilding);
                    break;
            }
        }

        return true;
    }

    // 생성
    public T Spawn<T>(Vector3 position, bool pooling = false) where T : BaseObject
    {
        string prefabName = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate(prefabName,pooling);
        go.name = prefabName;
        go.transform.position = position;

        BaseObject obj = go.GetComponent<BaseObject>();

        //Debug.Log($"{obj.name} + {obj.gameObject.activeSelf}");

        if (Register(obj) == false) 
        {
            // 등록 실패시 오류 알림
            Debug.LogError("Object Register Failed");
        };

        return obj as T;
    }

    // 파괴
    public void Despawn<T>(T obj, bool pooling) where T : BaseObject
    {
        Define.EObjectType objectType = obj.ObjectType;

        if (obj.ObjectType == Define.EObjectType.Creature)
        {
            Creature creature = obj as Creature;
            switch (creature.CreatureType)
            {
                case Define.ECreatureType.Hero:
                    Hero hero = creature as Hero;
                    Heroes.Remove(hero);
                    break;
                case Define.ECreatureType.Enemy:
                    Enemy enemy = creature as Enemy;
                    Enemies.Remove(enemy);
                    break;
            }
        }
        else if (obj.ObjectType == Define.EObjectType.Building)
        {
            Building building = obj as Building;
            switch (building.BuildingType)
            {
                case Define.EBuildingType.TargetBuilding:
                    TargetBuilding targetBuilding = building as TargetBuilding;
                    TargetBuildings.Remove(targetBuilding);
                    break;
                case Define.EBuildingType.EnemyBuilding:
                    EnemyBuilding enemyBuilding = building as EnemyBuilding;
                    EnemyBuildings.Remove(enemyBuilding);
                    break;
            }
        }

        Managers.Resource.Destroy(obj.gameObject, pooling);
    }

    public void Clear() 
    {
        Heroes.Clear();
        Enemies.Clear();
        TargetBuildings.Clear();
        EnemyBuildings.Clear();
    }
}
