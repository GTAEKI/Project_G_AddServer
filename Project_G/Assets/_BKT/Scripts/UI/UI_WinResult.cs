using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_WinResult : UI_Base
{
    private TextMeshProUGUI _text;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Register();

        TextMeshProUGUI[] _texts;
        _texts = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (var text in _texts) 
        {
            if (text.name == "Text_GetScrapCount") 
            {
                _text = text;
                break;
            }
        }

        return true;
    }

    public void DisplayScrap(int num)
    {
        _text.text = num.ToString();
    }

    protected override void Register()
    {
        Managers.UI.Register(this);
    }

    private void OnDestroy()
    {
        Managers.UI.Remove<UI_WinResult>();
    }
}
