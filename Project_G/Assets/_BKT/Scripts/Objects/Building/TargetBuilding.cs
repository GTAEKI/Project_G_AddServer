using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class TargetBuilding : Building,IDamageable
{
    private UI_WorldSpace_Hp UI_Building_Hp { get; set; }
    public float Hp { get; private set; } = 150f;
    public bool IsMissionStart { get; private set; } = false;
    private float _missionProgress = 0f;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BuildingType = Define.EBuildingType.TargetBuilding;
        UI_Building_Hp = GetComponentInChildren<UI_WorldSpace_Hp>();
        UI_Building_Hp.SetMaxHp(this.Hp);
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            IsMissionStart = true;
            Debug.Log("Hero입장");
            Managers.Obj.Despawn(other.GetComponent<Hero>(), true);
            StartCoroutine(FillMissionProgress());
        }
    }

    IEnumerator FillMissionProgress()
    {
        UI_MissionProgressBar missionBar = Managers.UI.Get<UI_MissionProgressBar>();
        while (_missionProgress <= missionBar.MaxMissionValue) 
        {
            _missionProgress += Time.deltaTime;
            missionBar.ReflectValue(_missionProgress);
            yield return null;
        }
        
        Managers.Game.Win();
    }

    public void Attacked(float damage)
    {
        Hp -= damage;
        UI_Building_Hp.ReflectUI(Hp);

        if (Hp <= 0)
        {
            Managers.Game.Lose();
        }
    }
}
