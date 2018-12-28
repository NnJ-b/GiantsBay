using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GiantMotor : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent navMeshAgent;
    public GiantStateController stateController;

    [Header("Navigation")]
    public float sightRange = 10f;
    

    PlayerController player;

    private void Start()
    {
        player = PlayerController.instance;
        stateController.onStateChangeCallBack += MoveToPlayer;
    }    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    void MoveToPlayer(EnemyStates state)
    {
        if(state == EnemyStates.Follow)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }
}