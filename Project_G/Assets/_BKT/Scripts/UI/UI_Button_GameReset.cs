using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button_GameReset : UI_MouseInteraction
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
        Managers.Game.GameResultReset();
        Managers.Round.ResetRound();
        Managers.HeroSpawn.ResetSpawnArea();
        Managers.BaseMap.SavedDataClear();
        Managers.Quest.QuestClear();
        Managers.Scrap.Init();
        Util.LoadScene(Define.EScene.TitleScene);
                
    }

    protected override void Register()
    {
        Managers.UI.Register(this);
    }

    private void OnDestroy()
    {
        Managers.UI.Remove<UI_Button_LeaveGame>();
    }
}
