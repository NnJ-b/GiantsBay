using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RangeWeapon", menuName = "Inventory/RangeWeapon")]
public class RangeWeapon : Equipment
{
    public GameObject projectile;
    GameObject projectileInstance = null;

    public override void Use()
    {
        base.Use();
        PlayerCombat.instance.animator.SetBool("RangeEquiped", true);
    }

    public override void Aim(PlayerCombat combat, EquipmentSlots slot, float angle)
    {
        base.Aim(combat, slot, angle);
        Debug.Log(angle);
    }

    public override void Fire()
    {
        base.Fire();
        if(projectileInstance != null)
        {
            projectileInstance.GetComponent<ArrowController>().fired = true;
        }
    }


    public override void Unequiped()
    {
        base.Unequiped();
        PlayerCombat.instance.animator.SetBool("RangeEquiped", false);
    }
}
