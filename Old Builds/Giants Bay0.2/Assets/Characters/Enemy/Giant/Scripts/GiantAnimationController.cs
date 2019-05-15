using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantAnimationController : MonoBehaviour
{
    [Header("References")]
    public GiantMotor motor;
    public Animator animator;
    [Header("Animation Controlls")]
    public float animationSmoothTime;


    

    void Update()
    {
        float speedPercent = motor.navMeshAgent.velocity.magnitude / motor.navMeshAgent.speed;
        animator.SetFloat("speedPercent", speedPercent, animationSmoothTime, Time.deltaTime);
    }
}
