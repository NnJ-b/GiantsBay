using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventoryable : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();
        bool sucess = PlayerInventory.instance.AddToInventory(item);
        if(sucess)
        {
            Destroy(gameObject);
        }
    }    
}
