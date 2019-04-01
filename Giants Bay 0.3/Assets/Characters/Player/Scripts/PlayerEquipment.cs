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

    public Equipment[] equiped;
    [SerializeField]
    public Transform LeftHand;
    [SerializeField]
    public Transform RightHand;
    PlayerInventory inventory;

    public void Start()
    {
        equiped = new Equipment[System.Enum.GetNames(typeof(EquipmentType)).Length];
        inventory = PlayerInventory.instance;
        onEquipmentChangedCallBack += changeGraphics;

    }

    public void EquipItem(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentType;
        Equipment oldItem = null;

        if(equiped[slotIndex] == null) //Item Slot Empty
        {
            equiped[slotIndex] = newItem;
            inventory.RemoveFromInventory(newItem);
            if (onEquipmentChangedCallBack != null) //Callback for equipmentChange
            {
                onEquipmentChangedCallBack.Invoke(newItem, oldItem);
            }
        }
        else //Item Slot Ocupied
        {
            oldItem = equiped[slotIndex];
            inventory.RemoveFromInventory(newItem);
            if (inventory.AddToInventory(oldItem)) //What to do if not to heavy
            {
                equiped[slotIndex] = newItem;
                if (onEquipmentChangedCallBack != null) //Callback for equipmentChange
                {
                    onEquipmentChangedCallBack.Invoke(newItem, oldItem);
                }
            }
            else //What to do if to heavy
            {
                inventory.AddToInventory(newItem); //returns item to inventory
            }
        }
    }

    private void changeGraphics(Equipment newItem, Equipment oldItem)
    {
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
            if (newItem.equipmentType == EquipmentType.PrimaryWeapon)
            {
                SetParent(RightHand, instance);
            }
            else if (newItem.equipmentType == EquipmentType.SecondaryWeapon)
            {
                SetParent(LeftHand, instance);
            }

            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
        }
    }

    private void SetParent(Transform hand, GameObject instance)
    {
        int childCount = hand.childCount;

        if (childCount > 0)
        {
            for (int i = 0; i < childCount; i++)
            {
                if (hand.transform.GetChild(i).tag == "Equipment")
                {
                    Destroy(hand.transform.GetChild(i).gameObject);
                }
            }
        }
        instance.transform.parent = hand.transform;

        instance.tag = "Equipment";
    }
}