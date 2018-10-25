﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GiantController : Interactable
{
    [Header("References")]
    public NavMeshAgent navMeshAgent;
    public Animator animator;

    private GameObject player;

    [Header("Animation Controls (DNM)")]
    public bool attacking = false;
    public bool endAttack = false;
    public bool startAttack = false;
    public bool attackReady = false;


    [Header("Controls")]
    public float speed =2.5f;
    public int damageAmount = 10;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent.speed = speed;
    }

    new void Update()
    {
        Move();

        Attack();
    }

    private void Move()
    {
        //close enough to attack?
        if (DistanceToPlayer() <= navMeshAgent.stoppingDistance*1.2f)
        {
            animator.SetBool("Attacking", true);
        }
        else
        {
            navMeshAgent.SetDestination(player.transform.position);
            animator.SetBool("Attacking", false);
        }
    }

    public override void Interact()
    {
        base.Interact();
    }

    private void Attack()
    {
        //stop moving
        if (attacking)
        {
            navMeshAgent.speed = 0f;
        }
        //start Moving
        else
        {
            navMeshAgent.speed = speed;
        }

        //Gets Attack Ready
        if (startAttack)
        {
            attackReady = true;
        }

        //Attacks
        if (endAttack && attackReady)
        {
            //check Distance after animation
            if (HitDetection())
            {
                //aplly Damage
                player.GetComponent<PlayerController>().TakeDamage(damageAmount);
            }
            attackReady = false;
        }
    }

    public float DistanceToPlayer()
    {
        if (player != null)
        {
            return Vector3.Distance(transform.position, player.transform.position);
        }
        else
        {
            //Place holder number
            return -2;
        }
    }


    public bool HitDetection()
    {
        if(DistanceToPlayer() <= navMeshAgent.stoppingDistance * 1.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
}
    
