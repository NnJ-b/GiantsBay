using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    public EquipmentType equipmentType;

    public virtual void Equip()
    {
    }
}

public enum EquipmentType { Helmet, Vest, PrimaryWeapon, SecondaryWeapon};

