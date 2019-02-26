using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    public EquipmentType equipmentType;

    public override void Use()
    {
        base.Use();
        Equip();
    }


    public virtual void Equip()
    {
        Debug.Log("Equiped: " + Name);
    }
}

public enum EquipmentType { Helmet, Vest, PrimaryWeapon, SecondaryWeapon};

