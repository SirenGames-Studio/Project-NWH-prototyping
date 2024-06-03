using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGS.Inventory
{
    [CreateAssetMenu(fileName = "New Item",menuName = "SGS/Inventory/Item")]
    public class ItemData_SO : ScriptableObject
    {
        public string id;

        [Header("Info")]
        public ItemType ItemBehaviour;
        public string DisplayName;
        public string Description;
        public Sprite Icon;
        public GameObject DropPrefab;

        [Header("Stackable")] 
        public bool CanStack;

        public int MaxStackAmount;
    }

    public enum ItemType
    {
        Resource,
        Equipable,
        Consumable,
    }

    public enum ItemVariety
    {
        None,
        Pickable,
        Openable
    }

}