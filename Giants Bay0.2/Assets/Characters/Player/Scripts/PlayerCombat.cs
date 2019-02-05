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

    [Header("References")]
    PlayerMotor playerMotor;

    PlayerEquipment playerEquipment;
    public PlayerAnimationController playerAnimationController;
    public Animator animator;
    public bool isAtacking;
    [Range(0, 1)]
    public float startAimSensitivity;

    private Vector3 mouseStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerEquipment = PlayerEquipment.instance;
        playerMotor = PlayerMotor.instance;
        playerEquipment.onEquipmentChangedCallBack += UpdateStats;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            AttackPrimary();
        }
        if (Input.GetMouseButtonDown(1))
        {            
            mouseStartPosition = Input.mousePosition;
            playerAnimationController.ikActive = true;
        }
        if(Input.GetMouseButton(1))
        {
            AimSecondary();
            animator.SetBool("Fire", false);
        }
        if (Input.GetMouseButtonUp(1))
        {
            FireSecondary();
            playerAnimationController.ikActive = false;

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
    void AimSecondary()
    {
        EquipmentSlots equipmentSlot = EquipmentSlots.SecondaryWeapon;
        if (playerEquipment.equipped[(int)equipmentSlot] != null)
        {
            
            //find angle
            Vector3 mouseDelta = Input.mousePosition - mouseStartPosition;

            if (mouseDelta.sqrMagnitude < startAimSensitivity)
            {
                return;            
            }

            float angle = Mathf.Atan2(mouseDelta.y, mouseDelta.x) * Mathf.Rad2Deg;
            angle -= 90;
            angle *= -1;
            


            float playerAngle = playerMotor.graphics.transform.localEulerAngles.y;

            //angle -= playerAngle;

            while (angle > 180)
            {
                angle =  angle - 360;
            }

            while (angle < -180)
            {
                angle = angle + 360;
            }
            playerAnimationController.IKAimLeftHand(angle);
            //send to Weapon
            playerEquipment.equipped[(int)equipmentSlot].Aim(this, equipmentSlot, angle);
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
        if(newItem != null)
        {
            PlayerStats.damage += newItem.damageMultiplyer;
            PlayerStats.armor += newItem.armorMultiplyer;
        }
        if(oldItem != null)
        {
            PlayerStats.damage -= oldItem.damageMultiplyer;
            PlayerStats.armor -= oldItem.armorMultiplyer;
        }

        Debug.Log("Damage: " + PlayerStats.damage.ToString() + "Armor: " + PlayerStats.armor.ToString());
    }

    public void ClearAnimatorBool()
    {
        animator.SetBool("MeleEquiped", false);
        animator.SetBool("RangeEquiped", false);
    }
}
