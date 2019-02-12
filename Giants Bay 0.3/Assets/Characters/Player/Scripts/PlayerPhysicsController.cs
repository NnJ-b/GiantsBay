using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsController : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public PlayerAnimationController animationController;

    [Header("Children")]
    public GameObject graphics;
    public GameObject cam;

    [Header("Colision Detection")]
    public float collisionDistance;
    public float CollisionWidth;
    public LayerMask collisionTypes;

    [Header("Movement")]
    public float speed = .3f;

    [Header("Animation")]
    public RaycastHit collisionHit;
    public bool colliding = false;
    public bool colShortAngleLeft = false;

    private PlayerInput input = new PlayerInput();

    public void Update()
    {
        Vector3 bestMovement = BestMovement();
        //set location and rotation
        LocRot(bestMovement);

        //set animation
        animationController.Anim(bestMovement,speed);

        //temp mouse movement
        if(Input.GetMouseButton(0))
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"));
        }
    }

    void LocRot(Vector3 bestMovement)
    {
        //move character
        transform.Translate(bestMovement);
        //rotate graphics
        graphics.transform.localRotation = Quaternion.LookRotation(bestMovement, Vector3.up);
        
    }  


    Vector3 BestMovement()
    {
        Vector3 movementDirection = input.GetDesiredMovement(transform);
        if (movementDirection != Vector3.zero)
        {
            Debug.DrawRay(transform.position, movementDirection, Color.green);

            //if Colision
            //if(Physics.Raycast(transform.position, movementDirection,out collisionHit, collisionDistance, collisionTypes)) //replaced with Sphearcast
            if (Physics.SphereCast(transform.position, CollisionWidth, movementDirection, out collisionHit, collisionDistance, collisionTypes,QueryTriggerInteraction.UseGlobal)) 
            {
                //set collision bool
                colliding = true;

                //find end of colision left
                Vector3 checkLeft = movementDirection;
                RaycastHit lastHit;
                //while (Physics.Raycast(transform.position, checkLeft, collisionDistance, collisionTypes))  //replaced with Sphearcast
                while (Physics.SphereCast(transform.position, CollisionWidth, checkLeft, out lastHit, collisionDistance, collisionTypes, QueryTriggerInteraction.UseGlobal)) 
                {
                    checkLeft = Quaternion.AngleAxis(-5f, Vector3.up) * checkLeft;
                }

                //find end of colision right
                Vector3 checkRight = movementDirection;
                //while (Physics.Raycast(transform.position, checkRight, collisionDistance, collisionTypes))  //replaced with Sphearcast
                while (Physics.SphereCast(transform.position, CollisionWidth, checkRight, out lastHit, collisionDistance, collisionTypes, QueryTriggerInteraction.UseGlobal)) 
                {
                    checkRight = Quaternion.AngleAxis(5f, Vector3.up) * checkRight;

                }

                //find shorter angle (left vs right)
                if (Vector3.Angle(checkLeft, input.GetDesiredMovement(transform)) <= Vector3.Angle(checkRight, input.GetDesiredMovement(transform)))
                {
                    //animation state
                    colShortAngleLeft = true;

                    Debug.DrawRay(transform.position, checkLeft, Color.red);
                    return transform.InverseTransformDirection(checkLeft * speed * Time.deltaTime);
                }
                else // if right is shorter
                {
                    //animation state
                    colShortAngleLeft = false;

                    Debug.DrawRay(transform.position, checkRight, Color.red);
                    return transform.InverseTransformDirection(checkRight * speed * Time.deltaTime);
                }
            }
            else //if no colision
            {
                //set collision bool
                colliding = false;

                //return input if no colision
                return transform.InverseTransformDirection(movementDirection * speed * Time.deltaTime);
            }
        }
        else //if no input
        {
            //set collision bool
            colliding = false;

            return Vector3.zero;
        }
    }    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, collisionDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CollisionWidth);
    }
}
