using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    #region singleton
    public static PlayerInventory instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple Inventory Scripts!!!");
        }
        instance = this;
    }
    #endregion

    public delegate void onItemChanged();
    public onItemChanged onItemChangedCallback;

    public float strength = 20f;
    public PlayerController playerController;
    public List<Item> inventoryItems = new List<Item>();

    public bool AddToInventory(Item item)
    {
        float weight = 0f;
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            weight += inventoryItems[i].weight;
        }
        if((weight += item.weight) > strength)
        {
            Debug.Log("To Heavy");
            return false;
        }
        else
        {
            inventoryItems.Add(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            return true;
        }         
    }

    public void RemoveFromInventory(Item item)
    {
        inventoryItems.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
