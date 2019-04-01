﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    public Mesh mesh;
    public Material material;
    public EquipmentType equipmentType;

    public override void Use()
    {
        base.Use();
        Equip();
    }


    public virtual void Equip()
    {
        PlayerEquipment.instance.EquipItem(this);
        Debug.Log("Equiped: " + Name);
    }
}

public enum EquipmentType { Helmet, Vest, PrimaryWeapon, SecondaryWeapon};

