using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SGS.Inventory;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int itemLimit = 6;
    public List<ItemData_SO> item_SO_List = new List<ItemData_SO>();

    public void AddItem(ItemData_SO item)
    {
        if (item_SO_List.Count < itemLimit)
        {
            item_SO_List.Add(item);
        }
        else
        {
            Debug.Log("item is full");
        }
    }
    
    public void RemoveItem(ItemData_SO item)
    {
        item_SO_List.Remove(item);
    }
}
