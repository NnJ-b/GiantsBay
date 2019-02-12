using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsController : MonoBehaviour
{
    [Header("Children")]
    public GameObject graphics;
    public GameObject cam;

    [Header("Colision Detection")]
    public float collisionDistance;
    public LayerMask collisionTypes;

    [Header("Movement")]
    public float speed = .3f;

    private PlayerInput input = new PlayerInput();

    public void Update()
    {
        //set location and rotation
        LocRot();
        
        //temp animation

        //temp mouse movement
        if(Input.GetMouseButton(0))
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"));
        }
    }

    void LocRot()
    {
        Vector3 bestMovement = BestMovement();
        transform.Translate(bestMovement);
        graphics.transform.localRotation = Quaternion.LookRotation(bestMovement, Vector3.up);
    }


    Vector3 BestMovement()
    {
        Vector3 movementDirection = input.GetDesiredMovement(transform);
        if (movementDirection != Vector3.zero)
        {
            Debug.DrawRay(transform.position, movementDirection, Color.green);

            //if Colision
            if(Physics.Raycast(transform.position, movementDirection, collisionDistance, collisionTypes))
            {
                //find end of colision left
                Vector3 checkLeft = movementDirection;
                while (Physics.Raycast(transform.position, checkLeft, collisionDistance, collisionTypes))
                {
                    checkLeft = Quaternion.AngleAxis(-5f, Vector3.up) * checkLeft;
                }

                //find end of colision right
                Vector3 checkRight = movementDirection;
                while (Physics.Raycast(transform.position, checkRight, collisionDistance, collisionTypes))
                {
                    checkRight = Quaternion.AngleAxis(5f, Vector3.up) * checkRight;

                }

                //find shorter angle (left vs right)
                if (Vector3.Angle(checkLeft, input.GetDesiredMovement(transform)) <= Vector3.Angle(checkRight, input.GetDesiredMovement(transform)))
                {
                    Debug.DrawRay(transform.position, checkLeft, Color.red);
                    return transform.InverseTransformDirection(checkLeft * speed * Time.deltaTime);
                }
                else // if right is shorter
                {
                    Debug.DrawRay(transform.position, checkRight, Color.red);
                    return transform.InverseTransformDirection(checkRight * speed * Time.deltaTime);
                }
            }
            else //if no colision
            {
                return transform.InverseTransformDirection(movementDirection * speed * Time.deltaTime);
            }
        }
        else //if no input;
        {
            return Vector3.zero;
        }
    }    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, collisionDistance);        
    }
}
