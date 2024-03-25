using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public ItemType ItemType;
}

public enum ItemType
{
    None,
    PickUP,
    Keys,
    Quests
}