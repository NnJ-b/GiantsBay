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
    public Rigidbody rb;
    public Animator animator;

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

            animator.SetFloat("Rotation", angleDiff);
            Debug.Log(angleDiff);
        }
    }
}
