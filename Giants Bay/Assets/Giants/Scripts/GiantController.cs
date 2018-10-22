using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GiantController : MonoBehaviour
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

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Attack();
    }

    private void Move()
    {
        if (DistanceToPlayer() <= navMeshAgent.stoppingDistance)
        {
            animator.SetBool("Attacking", true);
        }
        else
        {
            navMeshAgent.SetDestination(player.transform.position);
            animator.SetBool("Attacking", false);
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
        if(DistanceToPlayer() <= navMeshAgent.stoppingDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
}
    
