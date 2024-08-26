using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class Enemy : Creature
{
    protected IDamageable Target { get; private set; }
    public float Power { get; protected set; }
    public Define.EColorType ColorType { get; protected set; }
    protected UI_WorldSpace_Hp UI_EnemyHp { get; set; }
    private bool TriggerBuilding { get; set; }
    protected float hpMultiple;
    protected float powerMultiple;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        CreatureType = Define.ECreatureType.Enemy;
        hpMultiple = Managers.Round.GetCurrentRoundSetting().enemyHpMultiple;
        powerMultiple = Managers.Round.GetCurrentRoundSetting().enemyPowerMultiple;
        return true;
    }

    public override void SetInfo()
    {
        base.SetInfo();
    }

    protected void SetHp(float value) 
    {
        Hp = value;
        UI_EnemyHp = GetComponentInChildren<UI_WorldSpace_Hp>();
        UI_EnemyHp.SetMaxHp(Hp);
    }

    protected override void UpdateIdle()
    {
        Hero hero = FindClosestObject(Managers.Obj.Heroes) as Hero;
        if (hero != null)
        {
            Target = hero;
        }
        else 
        {
            Target = FindClosestObject(Managers.Obj.TargetBuildings) as TargetBuilding;
        }
        CreatureState = Define.ECreatureState.Move;
    }

    protected override void UpdateMove()
    {
        // 1. 체력 0이면 사망
        if (Hp <= 0)
        {
            CreatureState = ECreatureState.Die;
        }

        // 2. 점령이 시작됐다면 건물을 타격
        var targetBuilding = Target as TargetBuilding;
        if (TriggerBuilding == true && targetBuilding != null) 
        {
            animator.SetTrigger("Attack");
            transform.LookAt(targetBuilding.transform.position);
            return;
        }

        foreach (var building in Managers.Obj.TargetBuildings)
        {
            // 미션이 시작된 건물이 타겟
            if (building.IsMissionStart)
            {
                Target = building;
            }
        }

        // 3. 점령이 시작되지 않았다면 히어로를 찾아 공격
        Hero hero = FindRangeObject(2f, Managers.Obj.Heroes) as Hero;
        if (hero != null)
        {
            animator.SetTrigger("Attack");
            return;
        }

        // 4. 이동
        BaseObject obj = Target as BaseObject;
        Define.EFindPathResult result = FindPathAndMoveToCellPos(obj.transform.position);
    }

    protected override void UpdateAnimation()
    {
        switch (CreatureState)
        {
            case Define.ECreatureState.Idle:
                animator.SetBool("Move", false);
                break;
            case Define.ECreatureState.Move:
                animator.SetBool("Move", true);
                break;
            case Define.ECreatureState.Die:
                animator.SetBool("Die", true);
                break;
        }
    }

    protected override void UpdateDie()
    {
        TriggerBuilding = false;
        Managers.Sound.Play(ESound.Effect, "ZombieDie");
        Managers.Obj.Despawn(this,true);
    }

    public void CalDamage(float damage, Define.EColorType colorType) 
    {
        if (ColorType == colorType)
            damage *= 2;

        Hp -= damage;
        UI_EnemyHp.ReflectUI(Hp);
    }

    public void HitObject() 
    {
        Target.Attacked(Power);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TargetBuilding" && Target as TargetBuilding != null) 
        {
            TriggerBuilding = true;
        }
    }
}
