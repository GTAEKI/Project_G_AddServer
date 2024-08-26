using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button_Start : UI_MouseInteraction
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _EventHandler.OnClickHandler += OnPointerClick;
        Register();

        return true;
    }

    protected override void OnPointerClick(PointerEventData eventData) 
    {
        Util.LoadScene(Define.EScene.BaseScene);
    }

    protected override void Register()
    {
        Managers.UI.Register(this);
    }

    private void OnDestroy()
    {
        Managers.UI.Remove<UI_Button_Start>();
    }
}
