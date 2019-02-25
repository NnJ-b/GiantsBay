using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField]
    private float health;

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }
    
}
