
using System;
using SGS.Controls;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace SGS.Inventory
{

    public class Inventory : Singleton<Inventory> 
    {
        public ItemSlotUI[] UISlots;
        public ItemSlot[] Slots;
        

        public GameObject InventoryWindow;
        public Transform DropPosition;

        public GameObject ItemPopUpOption;

        [Header("Selected Item")] 
        private ItemSlot SelectedItem;
        private int _selectedItemIndex;
        [SerializeField] private TextMeshProUGUI selectedItemName;
        [SerializeField] private TextMeshProUGUI selectedItemDescription;
        [SerializeField] private TextMeshProUGUI selectedItemStatName;
        [SerializeField] private TextMeshProUGUI selectedItemStatValues;
      //  public GameObject UseButton;
      //  public GameObject EquipButton;
     //   public GameObject UnEquipButton;
      //  public GameObject DropButton;

        private int _curEquipIndex;

        public FirstPersonController playerController;
        public CameraController CameraController;

        [Header("Events")]
        public UnityEvent OnInventoryOpen;

        public UnityEvent OnInventoryClose;


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
            if(SelectedItem != null)
            {
                UISlots[_selectedItemIndex].SetSelected(false);
            }
            if (Slots[index].ItemData == null)
                return;

            SelectedItem = Slots[index];
            _selectedItemIndex = index;
            UISlots[index].SetSelected(true);

            selectedItemName.text = SelectedItem.ItemData.DisplayName;
            selectedItemDescription.text = SelectedItem.ItemData.Description;
            Debug.Log(SelectedItem.ItemData.DisplayName.ToString());


            //set stat value and stat name

           // UseButton.SetActive(SelectedItem.ItemData.ItemBehaviour == ItemType.Consumable);
           // EquipButton.SetActive(SelectedItem.ItemData.ItemBehaviour == ItemType.Equipable && !UISlots[index].Equipped);
           // UnEquipButton.SetActive(SelectedItem.ItemData.ItemBehaviour == ItemType.Equipable && UISlots[index].Equipped);
           // DropButton.SetActive(true);


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
          //  UseButton.SetActive(false);
          //  EquipButton.SetActive(false);
           // UnEquipButton.SetActive(false);
          //  DropButton.SetActive(false);
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
            if (SelectedItem.Quantity == 0)
            {
                if (UISlots[_selectedItemIndex].Equipped == true)
                    UnEquip(_selectedItemIndex);

                SelectedItem.ItemData = null;
                ClearSelectedItemWindow();
            }

            UpdateUI();
        }

        public void ItemPopUpWindow(Vector2 pos, bool popup)
        {
            if(popup == true)
            {
                ItemPopUpOption.SetActive(true);
                Vector3 adjustedPosition = ClampPopupWindowPosition(pos);
               // ItemPopUpOption.transform.position = pos;
            } else if(popup == false)
            {
                ItemPopUpOption.SetActive(false);
            }
                
        }

        private Vector3 ClampPopupWindowPosition(Vector3 position)
    {
        // Get the screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Adjust the popup window position to prevent it from going out of screen boundaries
        float popupWidth = ItemPopUpOption.GetComponent<RectTransform>().rect.width;
        float popupHeight = ItemPopUpOption.GetComponent<RectTransform>().rect.height;

        float clampedX = Mathf.Clamp(position.x, popupWidth / 2, screenWidth - popupWidth / 2);
        float clampedY = Mathf.Clamp(position.y, popupHeight / 2, screenHeight - popupHeight / 2);

        // Return the adjusted position
        return new Vector3(clampedX, clampedY, 0f);
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

}