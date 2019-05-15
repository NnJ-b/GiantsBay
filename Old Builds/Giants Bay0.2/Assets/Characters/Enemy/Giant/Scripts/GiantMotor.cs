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
    [Range(0, 10)]
    public float rotationSpeed = .1f;



    PlayerController player;

    private void Start()
    {
        player = PlayerController.instance;
        stateController.onStateChangeCallBack += MoveToPlayer;
    }

    private void Update()
    {
        float distanceSquared = (Mathf.Pow(transform.position.x - player.transform.position.x, 2) + Mathf.Pow(transform.position.y - player.transform.position.y, 2) + Mathf.Pow(transform.position.z - player.transform.position.z, 2));
        if(distanceSquared < Mathf.Pow(navMeshAgent.stoppingDistance,2))
        {
            navMeshAgent.updateRotation = false;
            RotationControl();
        }
        else
        {
            navMeshAgent.updateRotation = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    void RotationControl()
    {
        Vector3 direction = (player.transform.position - transform.position);
        Vector3 x = new Vector3(direction.x, 0f, direction.z);
        Quaternion lookRotation = Quaternion.LookRotation(x);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        Debug.Log(x);

    }

    void MoveToPlayer(EnemyStates state)
    {
        if(state == EnemyStates.Follow)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }
}