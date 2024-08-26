using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_MouseInteraction : UI_Base
{
    protected UI_EventHandler _EventHandler;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _EventHandler = this.GetOrAddComponent<UI_EventHandler>();
        return true;
    }

    protected virtual void OnPointerClick(PointerEventData eventData){}
    protected virtual void OnPointerDown(PointerEventData eventData){}
    protected virtual void OnPointerUp(PointerEventData eventData){}

    protected override void Register()
    {
    }
}
