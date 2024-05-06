
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Quest",menuName ="SGS/Quest",order = 1)]
public class Quest_SO : ScriptableObject
{
    public string ItemID;
    public string ItemName;
    public string ItemDescription;

    public bool isCompleted;

    public List<Objectives> objectives;

}

[System.Serializable]
public class Objectives
{
    public string Name;
    public bool complete;

    [TextArea(3,10)]
    public string QuestUpdate;

    [TextArea(10,10)]
    public string JournalEntry;
}
