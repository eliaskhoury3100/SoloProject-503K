using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler // makes the Slot clickable because it is an image not a button, and the others used for Hovering with Mouse
{

    public Image itemImage;
    public Image selectionFrame;
    public ItemData _itemData;

    public InventoryUI inventoryUI;

    private void Start()
    {
        inventoryUI = GetComponentInParent<InventoryUI>();
    }

    public ItemData ItemData
    {
        set
        {
            _itemData = value;
            UpdateUI(_itemData);
        }
        get
        {
            return _itemData;
        }
    }


    // set image sprite or reset the slot
    private void UpdateUI(ItemData itemData)
    {
        if (itemData == null)
        {
            //itemImage.sprite = null;
            itemImage.gameObject.SetActive(false);
        }
        else
        {
            itemImage.sprite = itemData.sprite;
            itemImage.gameObject.SetActive(true);
        }
    }

    [SerializeField] private const int PIXEL_OFFSET = 50;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_itemData == null)
            return;

        var contextMenuPos = new Vector3(eventData.position.x, eventData.position.y - PIXEL_OFFSET);
        inventoryUI._contextMenu.transform.position = contextMenuPos;
        inventoryUI._contextMenu.Open(_itemData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_itemData != null)
            inventoryUI.descriptionSection.text = _itemData.description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryUI.descriptionSection.text = ""; // clear the text data
    }
}
