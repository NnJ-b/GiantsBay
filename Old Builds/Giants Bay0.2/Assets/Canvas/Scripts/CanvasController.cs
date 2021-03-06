﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [Header("References")]
    public GameObject inventoryCanvas;
    public InventoryUIController InventoryController;

    public void InventoryToggle()
    {
        if(inventoryCanvas.activeSelf == false)
        {
            inventoryCanvas.SetActive(true);
            //InventoryController.UpdateUI();
        }
        else
        {
            inventoryCanvas.SetActive(false);
        }
    }

    private void Start()
    {
        HideAll();
    }

    private void HideAll()
    {
        inventoryCanvas.SetActive(false);
    }
}
