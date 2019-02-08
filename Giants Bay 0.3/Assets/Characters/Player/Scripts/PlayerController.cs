using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //references
    private PlayerMotor motor = new PlayerMotor();
    public Rigidbody rb;
    public GameObject parent;
    public Animator animator;
    //movement
    public float speed = 3f;


    private void Start()
    {
        
    }

    private void Update()
    {        
        animator.SetFloat("Velocity", rb.velocity.magnitude);
        MovementControlls();
        RotationControlls();
    }

    private void MovementControlls()
    {
        //movement
        rb.velocity = parent.transform.TransformDirection(motor.calculateVelocity(motor.calculateInput().x, motor.calculateInput().z, speed, rb) *Time.deltaTime);
    }

    private void RotationControlls()
    {
        #region movingcheck
        bool moving;
        if(rb.velocity.magnitude >.1f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        #endregion

        if (Input.GetMouseButton(0))
        {
            //stationary controlls
            Vector3 rotation = new Vector3();
            if (!moving)
            {
                rotation = transform.eulerAngles;
            }

            float rotationAmount = Input.GetAxis("Mouse X");
            parent.transform.localEulerAngles = new Vector3(0, motor.calculateRotation(parent.transform.eulerAngles.y, rotationAmount), 0);    

            if(!moving)
            {
                transform.eulerAngles = rotation;
            }
        }

        if (moving)
        {
            //sets graphics rotation based on velocity
            //transform.localRotation = Quaternion.LookRotation(transform.InverseTransformDirection(rb.velocity), Vector3.up);
            transform.localRotation = Quaternion.LookRotation(new Vector3(motor.calculateInput().x, 0, motor.calculateInput().z), Vector3.up);
            Debug.Log(rb.velocity);
        }
            
    }
}
