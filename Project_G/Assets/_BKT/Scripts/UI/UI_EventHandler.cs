using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public event Action<PointerEventData> OnClickHandler;
    public event Action<PointerEventData> OnPointerDownHandler;
    public event Action<PointerEventData> OnPointerUpHandler;

    public void OnPointerClick(PointerEventData eventData)
    {
        Managers.Sound.Play(Define.ESound.Effect, "ButtonClick");
        OnClickHandler?.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownHandler?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpHandler?.Invoke(eventData);
    }
}
