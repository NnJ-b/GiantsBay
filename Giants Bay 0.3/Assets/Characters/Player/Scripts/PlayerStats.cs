using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    PlayerEquipment playerEquipment;

    [SerializeField]
    private float health;

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
        //subtract old stats add new stats;
    }
}
