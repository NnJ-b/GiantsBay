using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    #region singleton
    public static PlayerCombat instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    PlayerEquipment playerEquipment;
    public Animator animator;
    public bool isAtacking;

    // Start is called before the first frame update
    void Start()
    {
        playerEquipment = PlayerEquipment.instance;
        playerEquipment.onEquipmentChangedCallBack += UpdateStats;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            AttackPrimary();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            AttackSecondary();
            animator.SetBool("Fire", false);
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            FireSecondary();
            animator.SetBool("Fire", true);
        }
    }

    void AttackPrimary()
    {
        EquipmentSlots equipmentSlot = EquipmentSlots.PrimaryWeapon;
        if (playerEquipment.equipped[(int)equipmentSlot] != null)
        {
            playerEquipment.equipped[(int)equipmentSlot].Attack(this, equipmentSlot);
        }
    }
    void AttackSecondary()
    {
        EquipmentSlots equipmentSlot = EquipmentSlots.SecondaryWeapon;
        if (playerEquipment.equipped[(int)equipmentSlot] != null)
        {
            playerEquipment.equipped[(int)equipmentSlot].Attack(this, equipmentSlot);
        }
    }
    void FireSecondary()
    {
        EquipmentSlots equipmentSlot = EquipmentSlots.SecondaryWeapon;
        if (playerEquipment.equipped[(int)equipmentSlot] != null)
        {
            playerEquipment.equipped[(int)equipmentSlot].Fire();
        }
    }


    void UpdateStats(Equipment newItem, Equipment oldItem)
    {
        Debug.Log("Updating Stats in PlayerCombat Script");
    }

    public void ClearAnimatorBool()
    {
        animator.SetBool("MeleEquiped", false);
        animator.SetBool("RangeEquiped", false);
    }
}
