using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public Sprite icon;
    public int weight;

    public virtual void Use()
    {
        Debug.Log("Using: " + Name);
    }
}