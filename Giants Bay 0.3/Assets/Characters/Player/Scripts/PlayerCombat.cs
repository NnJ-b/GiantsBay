using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerStats stats;
    PlayerEquipment equipment;

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
            if(mouseDelta(mousePos,Input.mousePosition)> switchToRangeSensativity)
            {
                attackType = AttackType.Range;
            }
            else
            {
                attackType = AttackType.Mele;
            }
        }
        if(Input.GetMouseButtonUp(1)) //Attack
        {
            if(attackType == AttackType.Mele)
            {
                if(equipment.equiped[(int)EquipmentType.PrimaryWeapon])
                {
                    Debug.Log("Mele Attack");
                }
            }
            if(attackType == AttackType.Range)
            {
                if (equipment.equiped[(int)EquipmentType.SecondaryWeapon])
                {
                    Debug.Log("Range Attack");
                }
            }
        }
    }

    private float mouseDelta(Vector2 mouseDownPos, Vector2 mouseUpPos)
    {
        return Vector2.Distance(mouseDownPos, mouseUpPos);
    }
}
