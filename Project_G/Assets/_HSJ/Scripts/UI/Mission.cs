using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mission : MonoBehaviour
{
    public TMP_Text LogText { get; private set; }
    public TMP_Text Progress { get; private set; }
    public int QuestID { get; private set; }
    private TMP_Text[] textArray;
    private Image image;
    // Need Refactoring

    void Awake()
    {
        Init();
    }
        
    void Init()
    {
        textArray = new TMP_Text[2];
        textArray = GetComponentsInChildren<TMP_Text>();
        image = GetComponentsInChildren<Image>()[1];
        LogText = textArray[0];
        Progress = textArray[1];
    }

    void Update()
    {
        SetText();
    }

    public void SetQuestID(int id)
    {
        QuestID = id;
        SetImage(QuestID);
    }

    public void SetText()
    {

        switch (QuestID)
        {
            case 2:
                SetProgressText(Managers.Quest.savedQuests[0].CurNum, Managers.Quest.savedQuests[0].ClearNum);
                break;
            case 3:

                SetProgressText(Managers.Quest.savedQuests[1].CurNum, Managers.Quest.savedQuests[1].ClearNum);
                break;
            case 4:
                SetProgressText(Managers.Quest.savedQuests[2].CurNum, Managers.Quest.savedQuests[2].ClearNum);

                break;
            case 5:
                SetProgressText(Managers.Quest.savedQuests[3].CurNum, Managers.Quest.savedQuests[3].ClearNum);

                break;
            default:
                break;
        }
    }

    public void SetImage(int id)
    {
        switch (id)
        {
            case 2:
                image.sprite = Managers.Resource.LoadFromResources<Sprite>("Base_building_tower_a_parent_img");
                break;
            case 3:
                image.sprite = Managers.Resource.LoadFromResources<Sprite>("Base_building_house_c_parent_img");
                break;
            case 4:
                image.sprite = Managers.Resource.LoadFromResources<Sprite>("Base_small_buildingE_parent_img");
                break;
            case 5:
                image.sprite = Managers.Resource.LoadFromResources<Sprite>("Base_building_turtle_a_parent_img");
                break;
            default:
                break;
        }
    }

    public void SetProgressText(int curNum, int clearNum)
    {
        Progress.text = $"{curNum} / {clearNum}";
    }


}
