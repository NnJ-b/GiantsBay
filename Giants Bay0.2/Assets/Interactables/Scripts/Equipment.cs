using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public Mesh mesh;
    public Material material;
    public EquipmentSlots equipmentSlot;

    public float armorMultiplyer;
    public float damageMultiplyer;

    PlayerCombat playerCombat;

    public Vector3 offset;
    public Quaternion rotationOffset;


    public override void Use()
    {
        base.Use();
        playerCombat = PlayerCombat.instance;
        PlayerEquipment.instance.equip(this);
    }
    //used for primary hand 
    public virtual void Attack(PlayerCombat combat, EquipmentSlots slot)
    {
    }
    public virtual void Aim(PlayerCombat combat, EquipmentSlots slot, float angle)
    {

    }
    //used range weapons
    public virtual void Fire()
    {
    }

    public virtual void Unequiped()
    {

    }
}

public enum EquipmentSlots { Head, Chest, Legs, PrimaryWeapon, SecondaryWeapon, Feet}
