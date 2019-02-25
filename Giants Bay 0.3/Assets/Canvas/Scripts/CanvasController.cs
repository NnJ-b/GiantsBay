using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject inventory;

    public void Start()
    {
        HideAll();
    }


    public void InventoryToggle()
    {
        if(inventory.activeSelf == true)
        {
            inventory.SetActive(false);
        }
        else
        {
            inventory.SetActive(true);
        }
    }

    public void HideAll()
    {
        inventory.SetActive(false);
    }
    
}
