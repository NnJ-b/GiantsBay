using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    #region singleton
    public static PlayerEquipment instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChangedCallBack;

    Equipment[] equipped;
    PlayerInventory inventory;

    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlots)).Length;
        equipped = new Equipment[numSlots];
        inventory = PlayerInventory.instance;
    }

    public void equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlot;
        Equipment oldItem = null;

        if(equipped[slotIndex] != null)
        {
            oldItem = equipped[slotIndex];
            inventory.RemoveFromInventory(newItem);
            if(inventory.AddToInventory(oldItem))
            {
                equipped[slotIndex] = newItem;
                if(onEquipmentChangedCallBack != null)
                {
                    onEquipmentChangedCallBack.Invoke(newItem,oldItem);
                }
            }
            else
            {
                inventory.AddToInventory(newItem);
            }
        }
        else
        {
            equipped[slotIndex] = newItem;
            inventory.RemoveFromInventory(newItem);
            if (onEquipmentChangedCallBack != null)
            {
                onEquipmentChangedCallBack.Invoke(newItem, oldItem);
            }
        }
    }

    public void Unequip(int slotIndex)
    {
        if(equipped[slotIndex] != null)
        {
            if (inventory.AddToInventory(equipped[slotIndex]))
            {
                Equipment oldItem = equipped[slotIndex];
                equipped[slotIndex] = null;
                if (onEquipmentChangedCallBack != null)
                {
                    onEquipmentChangedCallBack.Invoke(null, oldItem);
                }
            }
        }        
    }

    public void UnequipAll()
    {
        for (int i = 0; i < equipped.Length; i++)
        {
            Unequip(i);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
