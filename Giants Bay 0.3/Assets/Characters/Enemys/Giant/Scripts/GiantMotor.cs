using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GiantMotor : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    [Header("AI Controlls")]
    public float navUpdateTime;


    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(MoveToPlayer());
    }

    private IEnumerator MoveToPlayer()
    {
        navMeshAgent.SetDestination(player.position);
        yield return new WaitForSeconds(navUpdateTime);
    }

    private void AnimationState()
    {
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
    }

}
