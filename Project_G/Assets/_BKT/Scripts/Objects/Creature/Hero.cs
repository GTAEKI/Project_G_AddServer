using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class Hero : Creature, IDamageable
{
    [SerializeField]
    private float _hero_Hp = 100f;
    [SerializeField]
    private float _hero_Speed = 10f;

    public TargetBuilding TargetBuiding { get; private set; }
    private bool _isTakingOver = false;
    private UI_HeroHp UI_HeroHp { get; set; }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        CreatureType = Define.ECreatureType.Hero;
        UI_HeroHp = GameObject.Find("HeroHpBar").GetComponent<UI_HeroHp>();

        return true;
    }

    public override void SetInfo()
    {
        base.SetInfo();
        Speed = _hero_Speed;
        Hp = _hero_Hp;
        UI_HeroHp.SetMaxHp(_hero_Hp);
    }

    protected override void UpdateIdle()
    {

        if (_isTakingOver == true) 
        {
            Collider col = GetComponent<Collider>();
            return;
        }
        
        TargetBuilding building = FindClosestObject(Managers.Obj.TargetBuildings) as TargetBuilding;

        if (building != null) 
        {
            TargetBuiding = building;
            CreatureState = Define.ECreatureState.Move;
        }
    }

    protected override void UpdateMove()
    {
        Enemy enemy = FindRangeObject(2f, Managers.Obj.Enemies) as Enemy;
        if (enemy != null)
        {
            CreatureState = ECreatureState.Idle;
            return;
        }


        if (TargetBuiding == null)
        {
            CreatureState = Define.ECreatureState.Idle;
        }
        else 
        {
            Define.EFindPathResult result = FindPathAndMoveToCellPos(TargetBuiding.transform.position);
            switch (result)
            {
                case Define.EFindPathResult.Success:
                    break;
                case Define.EFindPathResult.Fail_NoPath:
                    break;
                case Define.EFindPathResult.Fail_LerpCell:
                    break;
                case Define.EFindPathResult.Fail_MoveTo:
                    break;
            }
        }
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

    public void Attacked(float damage) 
    {
        Hp -= damage;
        UI_HeroHp.ReflectUI(Hp);

        if (Hp <= 0)
        {
            CreatureState = Define.ECreatureState.Die;
            Managers.Game.Lose();
            return;
        }

        int rand = Random.Range(0,4);
        Managers.Sound.Play(ESound.Effect, $"AttackedHero{rand}");

        animator.SetTrigger("Attacked");

    }
}
