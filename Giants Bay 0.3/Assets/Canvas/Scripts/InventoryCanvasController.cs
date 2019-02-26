using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCanvasController : MonoBehaviour
{
    private PlayerInventory playerInventory;

    public Transform slotHolder;
    private SlotController[] slots;

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
        playerInventory = PlayerInventory.instance;

        playerInventory.onInventoryChangeCallBack += UpdateUI;

        slots = slotHolder.GetComponentsInChildren<SlotController>();
        UpdateUI();
    }



    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < playerInventory.items.Count)
            {
                slots[i].AddItem(playerInventory.items[i]);
            }
            else
            {
                slots[i].RemoveItem();
            }
        }
    }

}
