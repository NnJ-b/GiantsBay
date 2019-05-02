using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapons : Equipment
{
    public GameObject projectile;

    public override void Attack()
    {
        base.Attack();
        if (equipmentType == EquipmentType.PrimaryWeapon)
        {
            MeleAttack();
        }
        if (equipmentType == EquipmentType.SecondaryWeapon)
        {
            RangeAttack();
        }
    }

    public void MeleAttack()
    {
        Debug.Log("Mele Attack");
    }

    public void RangeAttack()
    {
        GameObject instance = Instantiate(projectile);
        instance.GetComponent<ProjectileColision>().damage = attack;
    }
}


