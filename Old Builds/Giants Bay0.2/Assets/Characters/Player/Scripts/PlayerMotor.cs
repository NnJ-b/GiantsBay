using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    #region singleton
    public static PlayerMotor instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [Header("References")]
    private Rigidbody rb;
    public PlayerController playerController;
    public GameObject graphics;
    public PlayerAnimationController animationController;

    [Range(.1f,1f)]
    public float speed = 1f;
    [Range(.1f, 1)]
    public float rotationSpeed;

    [HideInInspector]
    public Vector3 movement;
    [HideInInspector]
    public Quaternion childSavedRotation;

    public bool moving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        childSavedRotation = graphics.transform.rotation;
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRotation = 0f;

        if (Input.GetMouseButton(0))
        {
            xRotation = Input.GetAxis("Mouse X");

        }

        movement = new Vector3(x, 0, y) * speed;




        //moving controlls
        if (x != 0f || y != 0f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(x, 0, y), Vector3.up);
            transform.Rotate(Vector3.up, xRotation);
            
            //calculates rotation delta
            animationController.CalculateRotation(lookRotation);

            moving = true;
            graphics.transform.localRotation = lookRotation;
            //graphics.transform.localRotation = Quaternion.Slerp(graphics.transform.rotation, lookRotation, rotationSpeed);

            transform.Translate(movement);
            animationController.CalculateSpeed();
        }

        //stationary Controlls
        else
        {
            childSavedRotation = graphics.transform.rotation;
            transform.Rotate(Vector3.up, xRotation);
            graphics.transform.rotation = childSavedRotation;
            moving = false;
            
        }
    }
}
