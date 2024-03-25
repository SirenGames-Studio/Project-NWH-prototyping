using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGS.Inventory
{
    [CreateAssetMenu(fileName = "New Item",menuName = "SGS/Inventory/Item")]
    public class ItemData_SO : ScriptableObject
    {
        [Header("Info")]
        public ItemBehaviour ItemBehaviour;
        public new string DisplayName;
        public string Description;
        public Sprite Icon;
        public GameObject DropPrefab;

        [Header("Stackable")] 
        public bool CanStack;

        public int MaxStackAmount;
    }

    public enum ItemBehaviour
    {
        Resource,
        Equipable,
        Consumable,
    }

}