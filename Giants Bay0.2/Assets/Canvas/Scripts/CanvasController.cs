using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [Header("References")]
    public GameObject inventory;

    public void InventoryToggle()
    {
        if(inventory.activeSelf == false)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }

    private void Start()
    {
        HideAll();
    }

    private void HideAll()
    {
        inventory.SetActive(false);
    }
}
