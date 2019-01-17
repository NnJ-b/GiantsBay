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

    public Transform[] playerItemAttachmentPoints;

    public Equipment[] equipped;
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
                EquipPassed(newItem, slotIndex);
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
            EquipPassed(newItem, slotIndex);
            inventory.RemoveFromInventory(newItem);
            if (onEquipmentChangedCallBack != null)
            {
                onEquipmentChangedCallBack.Invoke(newItem, oldItem);
            }
        }
    }

    public void EquipPassed(Equipment newItem, int slotIndex)
    {
        equipped[slotIndex] = newItem;
        if (newItem.mesh != null)
        {
            GameObject go = new GameObject();

            GameObject instance = Instantiate(go);
            Destroy(go);
            MeshFilter filter = instance.AddComponent<MeshFilter>();
            MeshRenderer renderer = instance.AddComponent<MeshRenderer>();
            filter.mesh = newItem.mesh;
            renderer.material = newItem.material;

            Debug.Log(instance.transform.position);
            

            //cant reach if unequiped or if item has no mesh
            if (newItem.equipmentSlot == EquipmentSlots.PrimaryWeapon)
            {
                SetParent(newItem, 0, instance);
            }
            else if(newItem.equipmentSlot == EquipmentSlots.SecondaryWeapon)
            {
                SetParent(newItem, 1, instance);
            }
            
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
        }
        
    }

    private void SetParent(Equipment newItem, int playerAtachmentSlot, GameObject instance)
    {
        int childcount = playerItemAttachmentPoints[playerAtachmentSlot].transform.childCount;

        if (childcount > 0)
        {
            for (int i = 0; i < childcount; i++)
            {
                if(playerItemAttachmentPoints[playerAtachmentSlot].transform.GetChild(i).tag == "Equipment")
                {
                    Destroy(playerItemAttachmentPoints[playerAtachmentSlot].transform.GetChild(i).gameObject);
                }
            }
        }
        instance.transform.parent = playerItemAttachmentPoints[playerAtachmentSlot].transform;

        instance.tag = "Equipment";        
    }

    public void Unequip(int slotIndex)
    {
        if(equipped[slotIndex] != null)
        {
            if (inventory.AddToInventory(equipped[slotIndex]))
            {
                Equipment oldItem = equipped[slotIndex];
                equipped[slotIndex] = null;

                int playerAtachmentSlot = 0;
                if(oldItem.equipmentSlot == EquipmentSlots.SecondaryWeapon)
                {
                    playerAtachmentSlot = 1;
                }

                int childcount = playerItemAttachmentPoints[playerAtachmentSlot].transform.childCount;
                if (childcount > 0)
                {
                    for (int i = 0; i < childcount; i++)
                    {
                        Destroy(playerItemAttachmentPoints[playerAtachmentSlot].transform.GetChild(i).gameObject);
                    }
                }

                oldItem.Unequiped();
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
