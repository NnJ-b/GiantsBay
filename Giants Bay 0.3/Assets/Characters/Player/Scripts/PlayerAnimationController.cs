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
    private float collIKLerpWeight;
    [Range(0, 1)]
    public float IKlerpSpeed;



    public void Anim(Vector3 bestMovement, float speed)
    {
        //set animation states
        animator.SetFloat("Velocity", bestMovement.magnitude / speed / Time.deltaTime);
    }

    public void setFallLanding(bool grounded)
    {
        animator.SetBool("Grounded", grounded);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (physicsController.colliding)
        {
            //if (true) //check for more important animations like combat
            //{

                //lerp IK Weight
                collIKLerpWeight= Mathf.Lerp(collIKLerpWeight, collIKWeight, IKlerpSpeed);

                if (physicsController.colShortAngleLeft) //if moving Left from input
                {
                    animator.SetIKPosition(AvatarIKGoal.RightHand, physicsController.collisionHit.point);
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, collIKLerpWeight);
                }
                else //if moving right from input
                {
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, physicsController.collisionHit.point);
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, collIKLerpWeight);
                }

            //}
        }
        else //not colliding
        {
            collIKLerpWeight = Mathf.Lerp(collIKLerpWeight, 0, IKlerpSpeed);            
        }
    }
}
