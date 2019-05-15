using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MeleWeapon", menuName = "Inventory/MeleWeapon")]
public class MeleWeapons : Equipment
{

    public override void Use()
    {
        base.Use();
        PlayerCombat.instance.animator.SetBool("MeleEquiped", true);
    }

    public override void Attack(PlayerCombat combat, EquipmentSlots slot)
    {
        base.Attack(combat, slot);
        combat.animator.SetBool("MeleStart", true);
    }

    public override void Unequiped()
    {
        base.Unequiped();
        PlayerCombat.instance.animator.SetBool("MeleEquiped", false);
    }
}
