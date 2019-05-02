using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventoryable : MonoBehaviour
{
    public Item item;

    public virtual void Interact()
    {
        bool sucess = PlayerInventory.instance.AddToInventory(item);
        if(sucess)
        {
            Destroy(gameObject);
        }
    }
}
