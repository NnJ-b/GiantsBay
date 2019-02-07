using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor
{
    public Vector3 calculateVelocity(float horizontal, float vertical, float speed, Rigidbody rb)
    {

        Vector3 velocity = new Vector3(horizontal * speed, rb.velocity.y, vertical * speed);

        return velocity;
    }

    public float calculateRotation(float rotationAmount,float currentRotation)
    {
        float newRotation = currentRotation + rotationAmount;
        return newRotation;
    }
}
