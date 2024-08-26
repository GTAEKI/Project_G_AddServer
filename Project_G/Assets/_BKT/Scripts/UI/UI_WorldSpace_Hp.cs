using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WorldSpace_Hp : UI_WorldSpace
{
    private Slider _slider;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        _slider = GetComponentInChildren<Slider>();

        return true;
    }

    public void SetMaxHp(float maxHp)
    {
        if (_slider == null)
            _slider = GetComponentInChildren<Slider>();
        _slider.maxValue = maxHp;
        _slider.value = maxHp;
    }

    public void ReflectUI(float hp)
    {
        _slider.value = hp;
    }
}
