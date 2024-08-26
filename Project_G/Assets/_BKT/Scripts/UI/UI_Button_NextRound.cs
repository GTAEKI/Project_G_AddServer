using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button_NextRound : UI_MouseInteraction
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _EventHandler.OnClickHandler -= OnPointerClick;
        _EventHandler.OnClickHandler += OnPointerClick;

        return true;
    }

    private void OnEnable()
    {
        if (Managers.Round.HasNextRound() == false && Managers.Game.IsGameWin == true) 
        {
            Managers.Round.RoundAllClear();
            gameObject.SetActive(false);
        }
    }

    protected override void OnPointerClick(PointerEventData eventData)
    {
        Util.LoadScene(Define.EScene.BattleScene);
    }

    protected override void Register()
    {
        Managers.UI.Register(this);
    }

    private void OnDestroy()
    {
        Managers.UI.Remove<UI_Button_NextRound>();
    }
}
