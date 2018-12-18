using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    PlayerInventory inventory;
    public Transform itemsParent;

    SlotController[] slots;

    private void Start()
    {
        inventory = PlayerInventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<SlotController>();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.inventoryItems.Count)
            {
                slots[i].AddItem(inventory.inventoryItems[i]);
            }
            else
            {
                slots[i].RemoveItem();
            }
        }
    }
}
