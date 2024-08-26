using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeButton : MonoBehaviour
{
    private GameObject playPanel;

    public void Start()
    {
        playPanel = transform.parent.gameObject;
        if (Managers.Round.IsAllRoundClear == true) 
        {
            playPanel.SetActive(false);
        }
    }

    public void OnClickButton()
    {
        Util.LoadScene(Define.EScene.BattleScene);
    }
}
