using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GiantController : MonoBehaviour
{

    [Header("References")]
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    [Header("AI Controlls")]
    public float navUpdateTime;

    private GiantMotor motor = new GiantMotor();
    private Transform player;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(motor.MoveToPlayer(navMeshAgent, player, navUpdateTime));
    }

    private void Update()
    {
        motor.AnimationState(animator,navMeshAgent);
    }
}
