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
        //animation
    }

    private void MovementControlls()
    {
        //movement    replace input.getaxis when calculating with joysticks
        rb.velocity = parent.transform.TransformDirection(motor.calculateVelocity(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), speed, rb));
        //rb.MovePosition(parent.transform.position + motor.calculateMovement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), speed) * Time.deltaTime);
    }

    private void RotationControlls()
    {
        if (Input.GetMouseButton(0))
        {
            //stationary controlls
            if (rb.velocity.magnitude == 0)
            {
                Vector3 rotation = transform.eulerAngles;
                float rotationAmount = Input.GetAxis("Mouse X");
                parent.transform.localEulerAngles = new Vector3(0, motor.calculateRotation(parent.transform.eulerAngles.y, rotationAmount), 0);
                transform.eulerAngles = rotation;
            }

            //moving controlls
            else
            {
                float rotationAmount = Input.GetAxis("Mouse X");
                parent.transform.localEulerAngles = new Vector3(0, motor.calculateRotation(parent.transform.eulerAngles.y, rotationAmount), 0);
            }            
        }
        //sets graphics rotation based on velocity
        transform.localRotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
    }
}
