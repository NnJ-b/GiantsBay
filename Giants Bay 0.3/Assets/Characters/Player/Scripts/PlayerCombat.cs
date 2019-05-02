using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerStats stats;
    PlayerEquipment equipment;

    public GameObject aimPointParent;
    public GameObject aimPoint;

    public bool aiming;

    private enum AttackType {Mele,Range};
    private AttackType attackType;

    private Vector2 mousePos = Vector2.zero;
    [SerializeField]
    private float switchToRangeSensativity;

    private void Start()
    {
        equipment = PlayerEquipment.instance;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1)) //sets inital mouse position
        {
            mousePos = Input.mousePosition;
        }
        if(Input.GetMouseButton(1)) //aiming
        {
            if(mouseDelta(mousePos,Input.mousePosition)> switchToRangeSensativity && equipment.equiped[(int)EquipmentType.SecondaryWeapon])
            {
                attackType = AttackType.Range;
                aiming = true;
                aimRange(mousePos, Input.mousePosition);
            }
            else
            {
                attackType = AttackType.Mele;
            }
        }
        if(Input.GetMouseButtonUp(1)) //Attack
        {
            aiming = false;
            if(attackType == AttackType.Mele)
            {
                if (equipment.equiped[(int)EquipmentType.PrimaryWeapon])
                {
                    equipment.equiped[(int)EquipmentType.PrimaryWeapon].Attack();
                }
            }
            if(attackType == AttackType.Range)
            {
                if (equipment.equiped[(int)EquipmentType.SecondaryWeapon])
                {
                    equipment.equiped[(int)EquipmentType.SecondaryWeapon].Attack();
                }
            }
        }
    }

    private float mouseDelta(Vector2 mouseDownPos, Vector2 mouseUpPos)
    {
        return Vector2.Distance(mouseDownPos, mouseUpPos);
    }

    private void aimRange(Vector2 mouseDownPos, Vector2 mouseCurentPos)
    {
        //find angle
        Vector2 mouseDelta = mouseCurentPos - mouseDownPos;

        float angle = Mathf.Atan2(mouseDelta.y, mouseDelta.x) * Mathf.Rad2Deg;
        angle -= 90;
        angle *= -1;



        float playerAngle = transform.localEulerAngles.y;

        angle += playerAngle;

        while (angle > 180)
        {
            angle = angle - 360;
        }

        while (angle < -180)
        {
            angle = angle + 360;
        }

        aimPointParent.transform.eulerAngles = new Vector3(0, angle, 0);

        //send to Weapon
        //playerEquipment.equipped[(int)equipmentSlot].Aim(this, equipmentSlot, angle);
    }
}
