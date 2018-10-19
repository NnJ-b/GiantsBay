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
    public bool attacking;
    public bool checkHitDetection;

    [Header("Controls")]
    public float speed =2.5f;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent.speed = speed;
    }

    // Update is called once per frame
    void Update()
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

        if (attacking)
        {
            navMeshAgent.speed = 0f;
        }
        else
        {
            navMeshAgent.speed = speed;
        }

        if (checkHitDetection)
        {
            if(HitDetection())
            {
                //hit player
                Debug.Log("I'm Hit!");
            }
            checkHitDetection = false;
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
    
