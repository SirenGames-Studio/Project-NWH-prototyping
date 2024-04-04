using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using SGS.Controls;
using SGS.Inventory;
using UnityEngine;
using TMPro;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine.Events;
using Random = System.Random;

public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] UISlots;
    public ItemSlot[] Slots;

    public GameObject InventoryWindow;
    public Transform DropPosition;

    [Header("Selected Item")] private ItemSlot SeletedItem;
     private int _selectedItemIndex;
    [SerializeField] private TextMeshProUGUI selectedItemName;
    [SerializeField] private TextMeshProUGUI selectedItemDescription;
    [SerializeField] private TextMeshProUGUI selectedItemStatName;
    [SerializeField] private TextMeshProUGUI selectedItemStatValues;
    public GameObject UseButton;
    public GameObject EquipButton;
    public GameObject UnEquipButton;
    public GameObject DropButton;

    private int _curEquipIndex;

    public FirstPersonController playerController;

    [Header("Events")] 
    public UnityEvent OnInventoryOpen;

    public UnityEvent OnInventoryClose;
    
    //singleton
    public static Inventory Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InventoryWindow.SetActive(false);
        Slots = new ItemSlot[UISlots.Length];
        
        //initialize the slots
        for (int x = 0; x < Slots.Length; x++)
        {
            Slots[x] = new ItemSlot();
            UISlots[x].Index = x;
            UISlots[x].Clear();
        }
    }

    public void Toggle()
    {
        
    }

    public bool IsOpen()
    {
        return InventoryWindow.activeInHierarchy;
    }

    //adds the requested item to the player's inventory
    public void AddItem(ItemData_SO itemSO)
    {
        //is item stackable ?
        if (itemSO.CanStack)
        {
            ItemSlot slotToStackTo = GetItemStack(itemSO);
            if (slotToStackTo != null)
            {
                slotToStackTo.Quantity++;
                UpdateUI();
                return;
            }
        }
        
        // is there available empty slot
        ItemSlot emptySlot = GetEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.ItemData = itemSO;
            emptySlot.Quantity = 1;
            UpdateUI();
            Debug.Log("Adding Item to Empty Slot");
            return;
        }
        ThrowItem(itemSO);
    }

    private void ThrowItem(ItemData_SO itemSO)
    {
        Instantiate(itemSO.DropPrefab, DropPosition.position, Quaternion.Euler(Vector3.one));
    }

    void UpdateUI()
    {
        for (int x = 0; x < Slots.Length; x++)
        {
            if (Slots[x].ItemData != null)
            {
                UISlots[x].Set(Slots[x]);
            }
            else
            {
                UISlots[x].Clear();
            }
        }
    }

    private ItemSlot GetItemStack(ItemData_SO itemSO)
    {
        for (int x = 0; x < Slots.Length; x++)
        {
            if (Slots[x].ItemData == itemSO && Slots[x].Quantity < itemSO.MaxStackAmount)
                return Slots[x];
        }
    
        return null;
    }

    private ItemSlot GetEmptySlot()
    {
        for (int x = 0; x < Slots.Length; x++)
        {
            if (Slots[x].ItemData == null)
                return Slots[x];
        }
        
        return null;
    }

    public void SelectItem(int index)
    {
        
    }

    private void ClearSelectedItemWindow()
    {
        
    }

    public void OnUseButton()
    {
        
    }

    public void OnEquipButton()
    {
        
    } 
    
    public void OnUnEquipButton()
    {
        
    }

    public void OnDropButton()
    {
        
    }

    public void RemoveSelectedItem()
    {
        
    }

    public void RemoveItem(ItemData_SO itemSO)
    {
        
    }

    public bool HasItems(ItemData_SO itemSO)
    {
        return false;
    }
    
}

[System.Serializable]
public class ItemSlot
{
    public ItemData_SO ItemData;
    public int Quantity;
}
