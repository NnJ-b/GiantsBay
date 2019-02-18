using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GiantMotor
{
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

}
