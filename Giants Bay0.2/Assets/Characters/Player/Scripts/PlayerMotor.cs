using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    public PlayerController playerController;
    public GameObject graphics;

    [Range(1,10)]
    public float speed = 1f;
    [Range(.1f, 1)]
    public float rotationSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRotation = Input.GetAxis("Mouse X");
        Vector3 movement = new Vector3(x, 0, y) * speed;

        transform.Rotate(Vector3.up, xRotation);
        transform.Translate(movement);
        //rb.MovePosition(transform.localPosition + movement);

        if (x != 0f || y != 0f)
        {
            graphics.transform.localRotation = Quaternion.LookRotation(new Vector3(x, 0, y), Vector3.up);
            //graphics.transform.localRotation = Quaternion.Slerp(graphics.transform.rotation, Quaternion.LookRotation(new Vector3(x,0,y), Vector3.up), rotationSpeed);
        }        
    }
}
