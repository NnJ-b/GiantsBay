using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    PlayerInventory inventory;
    public Transform itemsParent;

    SlotController[] slots;

    private void OnEnable()
    {
        start();
    }

    private void Awake()
    {
        start();
    }

    private void start()
    {
        inventory = PlayerInventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<SlotController>();
        UpdateUI();
    }

    

    public void UpdateUI()
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
