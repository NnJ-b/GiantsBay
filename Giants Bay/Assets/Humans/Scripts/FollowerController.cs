using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerController : HumanClass {

    private PlayerController player;
    public bool targeted = false;

    [Header("Movement")]
    public NavMeshAgent navMeshAgent;
    public float IdleDistance = 5f;
    public float IdleUpdateSpeed = 5f;


    public enum State {Idle, Follow, Scavange}
    public State state;

   
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        StartCoroutine("Wander");
	}

    public void updateNavMeshAgent(Vector3 destination)
    {
        float x = Random.Range(-2.5f, 2.5f);
        Vector3 variance = new Vector3(x, 0, x);
        navMeshAgent.SetDestination(destination + variance);
    }

    IEnumerator Wander()
    {
        while(0 == 0)
        {
            if(state == State.Idle)
            {
                float x = Random.Range(-IdleDistance, IdleDistance);
                Vector3 variance = new Vector3(x, 0, x);
                Vector3 destination = transform.position + variance;
                navMeshAgent.SetDestination(destination);
                Debug.Log("Destination: " + destination);
            }
            yield return new WaitForSeconds(IdleUpdateSpeed);
        }
    }

}
