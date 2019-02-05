using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    #region singleton
    public static PlayerAnimationController instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public PlayerMotor playerMotor;
    public GameObject bowAimRotationParent;
    public GameObject bowAimTarget;
    public Rigidbody rb;
    public Animator animator;
    public bool ikActive;

    public float angleDiff;

    private float maxMagnitude;

    public bool rotating;


    private void Start()
    {
        maxMagnitude = (new Vector3(1, 0, 1) * playerMotor.speed).magnitude;
    }

    private void Update()
    {
        CalculateSpeed();
    }

    public void CalculateSpeed()
    {
        float speedPercent = playerMotor.movement.magnitude / maxMagnitude;
        animator.SetFloat("Speed", speedPercent);
    }

    public void CalculateRotation(Quaternion newLookRotation)
    {        
        if(!rotating && !playerMotor.moving)
        {
            Vector3 forwardA = playerMotor.graphics.transform.localRotation * Vector3.forward;
            Vector3 forwardB = newLookRotation * Vector3.forward;

            float angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
            float angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;

            angleDiff = Mathf.DeltaAngle(angleA, angleB);

            //animator.SetFloat("Rotation", angleDiff);
        }
    }

    public void IKAimLeftHand(float angle)
    {
        bowAimRotationParent.transform.eulerAngles = new Vector3(0, angle, 0);
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(ikActive)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, bowAimTarget.transform.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, bowAimTarget.transform.rotation);
            animator.SetLookAtPosition(bowAimTarget.transform.position);
            animator.SetLookAtWeight(1);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetLookAtWeight(0);
        }
       
    }
}