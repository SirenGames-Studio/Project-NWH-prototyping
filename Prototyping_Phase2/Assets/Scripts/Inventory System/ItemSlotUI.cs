using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SGS.Inventory;

public class ItemSlotUI : MonoBehaviour
{
    public Button Button;
    public Image Icon;
    public TextMeshProUGUI QuantityText;
    private ItemSlot _curItemSlot;
    private Outline _outline;

    public int Index;
    public bool Equipped;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        _outline.enabled = Equipped;
    }

    public void Set(ItemSlot slot)
    {
        _curItemSlot = slot;
        
        Icon.gameObject.SetActive(true);
        QuantityText.gameObject.SetActive(true);
        Icon.sprite = slot.ItemData.Icon;
        QuantityText.text = slot.Quantity > 1 ? slot.Quantity.ToString() : String.Empty;

        if (_outline != null)
        {
            _outline.enabled = Equipped;
        }
        
    }

    public void Clear()
    {
        _curItemSlot = null;
        
        Icon.gameObject.SetActive(false);
        QuantityText.text = string.Empty;
        QuantityText.gameObject.SetActive(false);
    }

    public void OnButtonClick()
    {
        Inventory.Instance.SelectItem(Index); 
    }
}
