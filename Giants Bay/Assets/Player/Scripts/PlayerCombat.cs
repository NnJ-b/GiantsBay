using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerCombat : MonoBehaviour {

    private PlayerController controller;
    public int health = 100;

    [Header("Combat")]
    public float baseDamage = 5f;
    public float damageAdder;
    private float damagePerHit;
    public float rangeMultiplyer = 1.2f;



    void Start ()
    {
        controller = GetComponent<PlayerController>();
        //ensure Range multiplyer is at least 1
        if (rangeMultiplyer < 1f)
        {
            rangeMultiplyer = 1f;
        }

        CalculateDamage();
    }

    public void Attack()
    {
        //attack
        Debug.Log("attacked");
        controller.motor.selected.TakeDamage(damagePerHit);
    }

    public bool HitDetection()
    {
        //close enough to hit?
        if (controller.motor.DistanceToEnemy() <= controller.motor.navMeshAgent.stoppingDistance * rangeMultiplyer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        //ouch
        health = health - damageAmount;
        Debug.Log(health);
        if (controller.healthBar != null)
        {
            controller.healthBar.value = health;
        }
        controller.inventory.AddBoosters(-damageAmount);
        controller.ChangeSize();
        SaveLoad.SaveInt("Health", health);
    }

    public void CalculateDamage()
    {
        damagePerHit = (baseDamage + damageAdder) * (1 + (controller.inventory.CalculateBoosterEffect() * controller.inventory.boostMultiplyer));
    }
}
