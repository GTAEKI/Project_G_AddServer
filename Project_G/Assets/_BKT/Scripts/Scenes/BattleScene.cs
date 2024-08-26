using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class BattleScene : InitBase
{
    [SerializeField]
    private int _maxEnemy = 1;
    private GameObject[] EnemyRespawnPoints;
    private GameObject[] TargetBuildings;
    private GameObject ClickToRespawnPanel;
    private GameObject HeroRespawn;

    Coroutine coEnemyRespawn;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Map.LoadMap("BattleMap");

        EnemyRespawnPoints = GameObject.FindGameObjectsWithTag(Define.EnemyRespawn);
        TargetBuildings = GameObject.FindGameObjectsWithTag(Define.TargetBuilding);
        ClickToRespawnPanel = GameObject.Find("ClickToRespawn");
        HeroRespawn = GameObject.Find("HeroRespawn");

        Managers.Game.OnSelectHeroSpawnPoint -= StartGame;
        Managers.Game.OnSelectHeroSpawnPoint += StartGame;
        Managers.Game.OnGameResult -= EndGame;
        Managers.Game.OnGameResult += EndGame;

        Managers.Sound.Play(Define.ESound.Bgm, "BattleScene");

        return true;
    }

    public void StartGame(Transform transform) 
    {
        ClickToRespawnPanel.SetActive(false);
        HeroRespawn.SetActive(false);

        #region Register Buildings
        foreach (var target in TargetBuildings)
        {
            TargetBuilding targetBuilding = target.GetOrAddComponent<TargetBuilding>();
            Managers.Obj.Register(targetBuilding);
        }

        foreach (var enemyPoint in EnemyRespawnPoints)
        {
            EnemyBuilding enemyBuilding = enemyPoint.GetOrAddComponent<EnemyBuilding>();
            Managers.Obj.Register(enemyBuilding);
        }
        #endregion

        Hero hero = Managers.Obj.Spawn<Hero>(transform.position);
        Managers.Map.MoveTo(hero, Managers.Map.World2Cell(hero.transform.position),hero.CreatureType, true);
        _maxEnemy = Managers.Round.GetCurrentRoundSetting().maxEnemyCount;
        coEnemyRespawn = StartCoroutine(CoCreateEnemy());

        Managers.Controller.Get<CinemachineController>().SwitchCamera(Define.EVirtualCamera.GameViewCamera, Managers.Game.GameStart);
    }

    IEnumerator CoCreateEnemy() 
    {
        float spawnTime = 3f;
        
        while (true)
        {
            foreach (var enemyBuilding in Managers.Obj.EnemyBuildings)
            {
                // 최대 적 숫자 설정
                if (Managers.Obj.Enemies.Count >= _maxEnemy)
                    yield return new WaitUntil(() => Managers.Obj.Enemies.Count < _maxEnemy);

                //int spawnChance = Random.Range(0, 10);
                //float spawnTime = Random.Range(0, 3);

                //if (spawnChance < 2)
                //    yield return new WaitForSeconds(spawnTime);

                enemyBuilding.CreateEnemy();
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void EndGame()
    {
        if (coEnemyRespawn != null) 
        {
            StopCoroutine(coEnemyRespawn);
        }
    }

    void OnDestroy()
    {
        Managers.Game.OnSelectHeroSpawnPoint -= StartGame;
        Managers.Game.OnGameResult -= EndGame;
    }
}
