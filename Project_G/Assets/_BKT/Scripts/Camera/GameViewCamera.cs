using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameViewCamera : InitBase
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Game.OnSelectHeroSpawnPoint -= MovePosition;
        Managers.Game.OnSelectHeroSpawnPoint += MovePosition;

        return true;
    }

    private void MovePosition(Transform heroSpawnArea) 
    {
        //transform.position = heroSpawnArea.position + new Vector3(-25,60,-30);
        transform.parent.position = heroSpawnArea.position + new Vector3(-25, 60, -30);
    }

    private void OnDestroy()
    {
        Managers.Game.OnSelectHeroSpawnPoint -= MovePosition;
    }
}
