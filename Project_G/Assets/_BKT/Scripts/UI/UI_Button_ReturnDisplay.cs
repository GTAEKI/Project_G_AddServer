using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button_ReturnDisplay : UI_MouseInteraction
{
    private UI_StandbyESC standbyPanel;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        standbyPanel = GameObject.FindAnyObjectByType<UI_StandbyESC>();
        _EventHandler.OnClickHandler += OnPointerClick;

        return true;
    }

    protected override void OnPointerClick(PointerEventData eventData)
    {
        standbyPanel.TogglePanel();
    }

    protected override void Register()
    {
        Managers.UI.Register(this);
    }

    private void OnDestroy()
    {
        Managers.UI.Remove<UI_Button_ReturnDisplay>();
    }
}
