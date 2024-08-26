using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
    public List<Quest> savedQuests;
    public bool isAllQuestClear = false;
    public QuestManager()
    {
        savedQuests = new();
    }


    public void SetQuestList(QuestData data)
    {
        Quest quest = new Quest(data.Name, data.ID, data.ClearNum, data.CurNum, data.IsClear);
        savedQuests.Add(quest);
    }

    public bool CheckQuestEmpty()
    {
        if(savedQuests.Count < 1)
        {
            return true;
        }
        return false;
    }

    public void UpdateQuestState(int buildingType)
    {
        for (int i = 0; i < savedQuests.Count; i++)
        {
            if (savedQuests[i].ID == buildingType)
            {
                savedQuests[i].Add();
                CheckQuestClear();
            }
        }
    }       

    public void CheckAllQuestClear()
    {
        foreach(Quest quest in savedQuests)
        {
            if(quest.IsClear == false)
            {
                return;
            }
        }
        isAllQuestClear = true;
    }


    public void CheckQuestClear()
    {
        foreach(Quest quest in savedQuests)
        {
            if(quest.CurNum >= quest.ClearNum)
            {
                quest.CheckClear();
            }
        }
    }

    public bool IsQuestClear(int iD)
    {
        foreach(Quest q in savedQuests)
        {
            if(q.ID == iD && q.CurNum >= q.ClearNum)
            {
                return false;
            }
        }
        return true;
    }

    public void QuestClear()
    {
        savedQuests.Clear();
        isAllQuestClear = false;
    }
}

public class Quest
{
    public Quest(string name, int iD, int clearNum, int curNum, bool isClear)
    {
        Name = name;
        ID = iD;
        ClearNum = clearNum;
        CurNum = curNum;
        IsClear = isClear;
    }

    public string Name { get; private set; }
    public int ID { get; private set; }
    public int ClearNum { get; private set; }
    public int CurNum { get; private set; }
    public bool IsClear { get; private set; }

    public void Add()
    {
        CurNum += 1;
    }

    public void CheckClear()
    {
        IsClear = true;
    }

}
