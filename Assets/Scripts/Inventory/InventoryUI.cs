using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform _inventoryPanel; // panel, parent containing all the children slots
    public ContextMenu _contextMenu;
    public TMP_Text descriptionSection;

    // method that refreshes UI by checking all the Inventory Items items-types inside the slots 
    public void RefreshUI(List<Inventory.InventoryItem> items)
    {
        // reset it as empty
        if (items.Count == 0)
        {
            _inventoryPanel.GetChild(0).GetComponent<InventorySlot>().ItemData = null;
        }

        for (int i = 0; i < items.Count; i++)
            _inventoryPanel.GetChild(i).GetComponent<InventorySlot>().ItemData = null;
            
        // fill it up again if items list is not empty
        for (int i = 0; i < items.Count; i++)
            _inventoryPanel.GetChild(i).GetComponent<InventorySlot>().ItemData = items[i].itemData;

    }

}
