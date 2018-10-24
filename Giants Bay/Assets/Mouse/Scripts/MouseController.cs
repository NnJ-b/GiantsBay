using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MouseController : Interactable {

    [Header("References")]
    public NavMeshAgent navMeshAgent;
    public float distanceToGetAway = 50f;
    public Animator animator;

    private bool stolen = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Interact()
    {
        base.Interact();
    }

    public override void Die()
    {
        //return Stolen Items
        base.Die();
    }

    new void Update ()
    {
        Move();
        Steal();

        if (stolen == true && DistanceToPlayer() > distanceToGetAway)
        {
            Destroy(gameObject);
        }

        //set animation speed
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
	}

    public void Move()
    {
        if (!stolen)
        {
            //chase player
            navMeshAgent.SetDestination(player.transform.position);
        }
        else
        {
            //run
            Vector3 runTo = transform.position + ((transform.position - player.transform.position) * 2f);
            navMeshAgent.SetDestination(runTo);
        }
            
    }

    public void Steal()
    {
        if (DistanceToPlayer() <= navMeshAgent.stoppingDistance*1.2f)
        {
            //stealSomething
            stolen = true;
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
}
