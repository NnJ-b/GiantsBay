using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    PlayerEquipment playerEquipment;

    [SerializeField]
    private float health;
    public float armor;
    public float attack;

    public void Start()
    {
        playerEquipment = PlayerEquipment.instance;
        playerEquipment.onEquipmentChangedCallBack += UpdateStats;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }

    public void UpdateStats(Equipment newItem, Equipment oldItem)
    {
        //attack
        if(oldItem != null)
        {
            attack -= oldItem.attack;
            attack += newItem.attack;
        }

        //armor
        if(oldItem != null)
        {
            armor -= oldItem.armor;
            armor += oldItem.armor;
        }
    }
}
