using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController: MonoBehaviour
{

    [Header("References")]
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    [Header("AI Controlls")]
    public float navUpdateTime;
    [Header("Attacking")]
    public float damageAmount;
    public float attackRange;
    private bool attacking;



    private EnemyMotor motor = new EnemyMotor();
    private EnemyAttack attack = new EnemyAttack();
    private Transform player;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(motor.MoveToPlayer(navMeshAgent, player, navUpdateTime));
    }

    private void Update()
    {
        motor.AnimationState(animator,navMeshAgent);
        if (attack.PlayerInRange(player, transform, attackRange))
        {
            if(!attacking)
            {
                animator.SetBool("Attacking", true);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void StartAttack()
    {
        motor.StopNavAgent(navMeshAgent);
        attacking = true;
        animator.SetBool("Attacking", false);
    }

    public void EndAttack()
    {
        motor.StartNavAgent(navMeshAgent);
        attacking = false;
        if (attack.PlayerInRange(player, transform, attackRange))
        {
            attack.HitPlayer(player, damageAmount);

        }
    }
}
