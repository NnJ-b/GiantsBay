using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public Mesh mesh;
    public Material material;
    public EquipmentSlots equipmentSlot;

    public float armorMultiplyer;
    public float damageMultiplyer;

    

    public override void Use()
    {
        base.Use();
        PlayerEquipment.instance.equip(this);
    }
}

public enum EquipmentSlots { Head, Chest, Legs, PrimaryWeapon, SecondaryWeapon, Feet}
