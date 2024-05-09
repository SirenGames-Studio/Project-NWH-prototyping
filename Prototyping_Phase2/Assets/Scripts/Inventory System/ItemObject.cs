using SGS.Inventory;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData_SO ItemData;
    
    public string GetInteractPrompt()
    {
        return string.Format("Pickup " + ItemData.DisplayName);
    }

    public void OnInteract()
    {
        Inventory.Instance.AddItem(ItemData);   
        Destroy(gameObject);
    }
}
