using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : InitBase
{
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        Managers.Sound.Play(Define.ESound.Bgm, "TitleScene");

        return true;
    }
}
