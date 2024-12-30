using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenu : MonoBehaviour
{

    [SerializeField] Button discardBtn, useBtn, cancelBtn;
    [SerializeField] Inventory _inventory;

    public void Open(ItemData itemData)
    {
        gameObject.SetActive(true);

        // Remove all previous listeners to avoid duplicates at every call of this method
        useBtn.onClick.RemoveAllListeners();
        discardBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.RemoveAllListeners();

        useBtn.onClick.AddListener(() =>
        {
            _inventory.RemoveItem(itemData); // mch lezim nhot condition if count < 1, else dec count ???
            gameObject.SetActive(false); // remove the context Menu after the click
            Debug.Log("I used the item" + itemData.name);
            // I should add itemData.Use() to use the item!
        });

        discardBtn.onClick.AddListener(() =>
        {
            _inventory.RemoveItem(itemData);
            gameObject.SetActive(false); // remove the context Menu after the click
            Debug.Log(itemData.name + "is removed from inventory");
        });

        cancelBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

    }

    /* Note on gameObject SetActive:
     * When SetActive is false, and one of the scripts attached to the gameObject are called in an other script,
     * the method in the script works as long as it's not Start, Update, etc.
     */
}
