using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestDataBase : ScriptableObject
{
    public List<QuestData> quests;
}

[Serializable]
public class QuestData
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public int ClearNum { get; private set; }
    [field: SerializeField]
    public int CurNum { get; private set; }
    [field: SerializeField]
    public bool IsClear { get; private set; }

}
