using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMotor
{
    float navSpeed;

    public IEnumerator MoveToPlayer(NavMeshAgent navMeshAgent, Transform player, float navUpdateTime)
    {
        while(true)
        {
            navMeshAgent.SetDestination(player.position);
            yield return new WaitForSeconds(navUpdateTime);
        }
        
    }

    public void AnimationState(Animator animator, NavMeshAgent navMeshAgent)
    {
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
    }

    public void StopNavAgent(NavMeshAgent navMeshAgent)
    {
        navSpeed = navMeshAgent.speed;
        navMeshAgent.speed = 0f;    
        
    }
    public void StartNavAgent(NavMeshAgent navMeshAgent)
    {
        navMeshAgent.speed = navSpeed;
    }

}
