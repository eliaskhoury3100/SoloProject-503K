using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable] // allows to be serialized
    public class InventoryItem
    {
        public ItemData itemData; // SO
        public int count;
    }

    [System.Serializable] // In Inventory, I don't want to save the entire ItemData, just ID
    public class Data
    {
        public List<string> itemIds;
        // should also save count when implementing counts, now we are assuming 1 instance of each item
    }


    public InventoryUI inventoryUI;
    private List<InventoryItem> items; // I need the list to save items+count on RAM not Disk!                           
    public List<ItemData> itemsOnStart; // list of ItemData to fill
    [SerializeField] List<ItemData> allItems;


    private void Start()
    {
        items = new List<InventoryItem>(8); // initialize items list with 8 slots

        foreach (var item in itemsOnStart)
            AddItem(item); // add the ItemData to the list of InventoryItem

    }


    // Add new item given ItemData and count
    public void AddItem(ItemData itemData, int count = 1)
    {
        // add a new instance of InventoryItem
        items.Add(new InventoryItem()
        {
            itemData = itemData,
            count = count
        });
        inventoryUI.RefreshUI(items);
    }


    // Remove item via two methods: give itemData or index in the list

    public void RemoveItem(ItemData itemData)
    {
        // Find a ref to the item inside the list such as it has itemData
        var itemRef = items.Find(_ => _.itemData == itemData);
        if (itemRef != null)
        {
            items.Remove(itemRef); // remove the item using its ref
        }
        inventoryUI.RefreshUI(items);
    }

    public void RemoveItem(int index)
    {
        if (items[index] != null)
        {
            items.RemoveAt(index); // remove the item using its index
        }
        inventoryUI.RefreshUI(items);
    }


    public Data Save()
    {
        var data = new Data();
        data.itemIds = new List<string>();

        foreach (var item in this.items)
        {
            data.itemIds.Add(item.itemData.id);
        }
        return data;
    }

    public void Load(Data data)
    {
        items.Clear();

        //Dictionary<string, ItemData> lookup = new Dictionary<string, ItemData>();

        foreach (var id in data.itemIds)
        {
            var itemLookup = allItems.Find(_ => _.id == id);
            if (itemLookup != null)
            {
                AddItem(itemLookup, 1); // 1 is passed by default anyway
            }
        }

    }

}

/* Notes on Find method with lists:
 * Ex: var itemRef = items.Find(_ => _.count = 4); finds item with count=4
 * This uses a lambda function to search in the list, "_" is a placeholder
 * Typically, we'd write as such:
 * ItemType itemRef = null; // Replace ItemType with the actual type of items in the list -> InventoryItem
 * foreach (var item in items)
 {
    if (item.itemData == itemData)
    {
        itemRef = item;
        break;
    }
 }
 */
