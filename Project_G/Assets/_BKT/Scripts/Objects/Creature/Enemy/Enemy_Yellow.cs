using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Yellow : Enemy
{
    [SerializeField]
    private  float _Y_HP = 500f;
    [SerializeField]
    private  float _Y_Speed = 50f;
    [SerializeField]
    private  float _Y_Power = 10f;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        ColorType = Define.EColorType.Yellow;

        return true;
    }

    public override void SetInfo()
    {
        base.SetInfo();
        
        SetHp(_Y_HP * hpMultiple);
        Power = _Y_Power * powerMultiple;
        Speed = _Y_Speed;
    }
}
