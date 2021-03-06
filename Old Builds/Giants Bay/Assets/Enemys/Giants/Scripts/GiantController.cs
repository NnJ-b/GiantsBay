﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GiantController : Interactable
{
    [Header("References")]
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public GiantHouseController home;
    private GameObject player;

    [Header("Animation Controls (DNM)")]
    public bool attacking = false;
    public bool endAttack = false;
    public bool startAttack = false;
    public bool attackReady = false;

    [Header("Slaves")]
    public GameObject slave;
    public int slaveCount = 1;
    private GameObject[] slaves;

    [Header("Controls")]
    public float speed =2.5f;
    public int damageAmount = 10;
    public float playerGetAwayDistance;
    public bool GoingHome = false;

    new void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent.speed = speed;
        slaves = new GameObject[slaveCount];
        for (int i = 0; i < slaveCount; i++)
        {
            slaves[i] =  Instantiate(slave, transform.position, Quaternion.identity);
            slaves[i].GetComponent<SlaveController>().master = this;
            slaves[i].GetComponent<SlaveController>().navMeshAgent.speed = (navMeshAgent.speed * Random.Range(.8f,1.2f));
        }
    }

    new void Update()
    {
        Move();
        Attack();
    }

    public override void Die()
    {
        for (int i = 0; i < slaves.Length; i++)
        {
            if (slaves[i] != null)
            {
                slaves[i].GetComponent<SlaveController>().FreeSlaves();
            }
        }
        if(home != null)
        {
            home.Owned = false;
        }

        base.Die();
       
    }

    private void Move()
    {
        //close enough to attack?
        if (DistanceToPlayer() <= navMeshAgent.stoppingDistance*1.2f)
        {
            animator.SetBool("Attacking", true);
            GoingHome = false;
        }
        else if(DistanceToPlayer() >= playerGetAwayDistance)
        {
            GoHome();
            GoingHome = true;
        }
        else
        {
            navMeshAgent.SetDestination(player.transform.position);
            animator.SetBool("Attacking", false);
            GoingHome = false;
        }
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
                player.GetComponent<PlayerController>().combat.TakeDamage(damageAmount);
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

    private void GoHome()
    {
        if(home != null)
        {
            if (Vector3.Distance(transform.position, home.transform.position) < home.interactableRange)
            {
                home.occupied = true;
                for (int i = 0; i < slaves.Length; i++)
                {
                    Destroy(slaves[i]);
                }
                Destroy(gameObject);
            }
            else
            {
                navMeshAgent.SetDestination(home.transform.position);
            }
        }
    }
}
    
