using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("References")]
    public PlayerPhysicsController physicsController;
    public Animator animator;
    [Header("IK Controlls")]
    public float collIKWeight;



    public void Anim(Vector3 bestMovement, float speed)
    {
        //set animation states
        animator.SetFloat("Velocity", bestMovement.magnitude / speed / Time.deltaTime);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (physicsController.colliding)
        {
            //if (true) //check for more important animations like combat
            //{
                if (physicsController.colShortAngleLeft) //if moving Left from input
                {
                    animator.SetIKPosition(AvatarIKGoal.RightHand, physicsController.collisionHit.point);
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, collIKWeight);
                }
                else //if moving right from input
                {
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, physicsController.collisionHit.point);
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, collIKWeight);
                }

            //}
        }
    }
}
