using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button_GoBaseScene : UI_MouseInteraction
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _EventHandler.OnClickHandler += OnPointerClick;
        return true;
    }

    protected override void OnPointerClick(PointerEventData eventData)
    {
        Managers.Game.Result();
        Util.LoadScene(Define.EScene.BaseScene);
    }

    protected override void Register()
    {
        Managers.UI.Register(this);
    }

    private void OnDestroy()
    {
        Managers.UI.Remove<UI_Button_GoBaseScene>();
    }
}
