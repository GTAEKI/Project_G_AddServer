using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LoseResult : UI_Base
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Register();
        return true;
    }

    protected override void Register()
    {
        Managers.UI.Register(this);
    }

    private void OnDestroy()
    {
        Managers.UI.Remove<UI_LoseResult>();
    }
}
