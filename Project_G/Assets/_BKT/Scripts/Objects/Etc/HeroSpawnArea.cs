using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawnArea : InitBase
{
    public override bool Init()
    {
        if(base.Init()==false) 
            return false;

        if (Managers.HeroSpawn.IsSpawnAreaActive(this.gameObject) == false) 
        {
            gameObject.SetActive(false);
        }

        return true;
    }

    public void SelectedArea() 
    {
        Managers.HeroSpawn.SetUsedSpawnArea(this.gameObject);
        Managers.Game.SelectHeroSpawnPoint(transform);
        Managers.Sound.Play(Define.ESound.Effect, "ButtonClick");
    }
}
