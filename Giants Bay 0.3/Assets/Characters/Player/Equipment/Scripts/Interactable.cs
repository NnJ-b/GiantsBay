using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Item item;

    public virtual void Interact()
    {
        Debug.Log("Interacted With: " + transform.name);
        bool sucess = PlayerInventory.instance.AddToInventory(item);
        if(sucess)
        {
            Destroy(gameObject);
        }
    }
}
