using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroHp : UI_Base
{
    private Slider _slider;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _slider = GetComponent<Slider>();

        return true;
    }

    public void SetMaxHp(float maxHp)
    {
        _slider.maxValue = maxHp;
        _slider.value = maxHp;
    }

    public void ReflectUI(float hp)
    {
        _slider.value = hp;
    }

    protected override void Register()
    {
        Managers.UI.Register(this);
    }

    private void OnDestroy()
    {
        Managers.UI.Remove<UI_HeroHp>();
    }
}
