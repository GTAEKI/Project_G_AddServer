using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StandbyESC : UI_Base
{
    private Transform _panel;
    private bool _isPanelActive = false;
    private TextMeshProUGUI _text;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _panel = transform.GetChild(0);
        TogglePanel();
        
        TextMeshProUGUI[] _texts;
        _texts = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (var text in _texts)
        {
            if (text.name == "Text_CurrentScrap")
            {
                _text = text;
                break;
            }
        }

        TogglePanel();

        return true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisplayCurrentScrap();
            TogglePanel();
        }
    }

    public void TogglePanel() 
    {
        if (_isPanelActive == false)
            _panel.gameObject.SetActive(true);
        else
            _panel.gameObject.SetActive(false);

        _isPanelActive = !_isPanelActive;
    }

    private void DisplayCurrentScrap()
    {
        _text.text = Managers.Scrap.GetCurrentScrapText();
    }


    protected override void Register()
    {
        Managers.UI.Register(this);
    }
}
