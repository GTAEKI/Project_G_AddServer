using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameClearScene : InitBase
{
    [SerializeField]
    private Image panelImage;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        Managers.Sound.Play(Define.ESound.Bgm, "GameClearScene");
        StartCoroutine(ChangeAlpha());
        return true;
    }


    IEnumerator ChangeAlpha()
    {
        Managers.Quest.isAllQuestClear = false;
        Color c = new Color(0f, 0f, 0f, 1f);
        panelImage.color = c;
        while (0 < c.a)
        {
            c.a -= Time.deltaTime * 0.5f;
            panelImage.color = c;
            yield return null;
        }
    }
}
