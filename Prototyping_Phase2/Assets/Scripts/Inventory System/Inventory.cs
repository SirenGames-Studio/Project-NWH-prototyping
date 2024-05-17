
using System;
using SGS.Controls;
using SGS.Inventory;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] UISlots;
    public ItemSlot[] Slots;

    public GameObject InventoryWindow;
    public Transform DropPosition;

    [Header("Selected Item")] private ItemSlot SelectedItem;
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
    public MouseLook mouseController;

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
        
        ClearSelectedItemWindow();
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
        if(Slots[index].ItemData == null)
            return;

        SelectedItem = Slots[index];
        _selectedItemIndex = index;

        selectedItemName.text = SelectedItem.ItemData.DisplayName;
        selectedItemDescription.text = SelectedItem.ItemData.Description;
        
        
        //set stat value and stat name

        UseButton.SetActive(SelectedItem.ItemData.ItemBehaviour == SGS.Inventory.ItemBehaviour.Consumable);
        EquipButton.SetActive(SelectedItem.ItemData.ItemBehaviour == SGS.Inventory.ItemBehaviour.Equipable && !UISlots[index].Equipped);
        UnEquipButton.SetActive(SelectedItem.ItemData.ItemBehaviour == SGS.Inventory.ItemBehaviour.Equipable && UISlots[index].Equipped);
        DropButton.SetActive(true);

        
    }

    private void ClearSelectedItemWindow()
    {
        //clear text elements
        SelectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValues.text = string.Empty;
        
        //disable item
        UseButton.SetActive(false);
        EquipButton.SetActive(false);
        UnEquipButton.SetActive(false);
        DropButton.SetActive(false);
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
        ThrowItem(SelectedItem.ItemData);
        RemoveSelectedItem();
    }

    public void RemoveSelectedItem()
    {
        SelectedItem.Quantity--;
        if(SelectedItem.Quantity == 0)
        {
            if (UISlots[_selectedItemIndex].Equipped == true)
                    UnEquip(_selectedItemIndex);

            SelectedItem.ItemData = null;
            ClearSelectedItemWindow();
        }

        UpdateUI(); 
    }

    private void UnEquip(int selectedItemIndex)
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
