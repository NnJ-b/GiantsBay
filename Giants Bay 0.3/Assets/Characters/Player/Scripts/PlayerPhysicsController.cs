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
    public RaycastHit collisionHit;
    public bool colliding = false;
    public bool colShortAngleLeft = true;

    [Header("Movement/Rotation")]
    public float speed = .3f;
    private float xRotation;
    public float maxClimbableAngle = 45f;
    public bool moveable = true;

    [Header("Gravity Controls")]
    public float footOffset;
 

    private PlayerInput input = new PlayerInput();

    public void Update()
    {
        Vector3 bestMovement = BestMovement();

        //set location and rotation
        LocRot(bestMovement);

        //apply gravity
        ApplyGravity();

        //set animation
        animationController.Anim(bestMovement, speed);

        //TEMP mouse movement
        if (Input.GetMouseButton(0))
        {
            xRotation = Input.GetAxis("Mouse X");
        }
        else
        {
            xRotation = 0f;
        }        
    }

    void LocRot(Vector3 bestMovement)
    {
        if(moveable)
        {
            //move character
            transform.Translate(bestMovement);
        }
        
       
        //rotate camera
        if(input.GetDesiredMovement(transform) == Vector3.zero || !moveable) //stationary controlls
        {
            Quaternion graphicsSavedLocation = graphics.transform.rotation;
            transform.Rotate(Vector3.up, xRotation);
            graphics.transform.rotation = graphicsSavedLocation;         
        }
        else //moving
        {            
            transform.Rotate(Vector3.up, xRotation);
            //rotate graphics in direction of movement
            Vector3 noYMovement = bestMovement;
            noYMovement.y = 0;
            graphics.transform.localRotation = Quaternion.LookRotation(noYMovement, Vector3.up);
        }  
    }

    void ApllyJump()
    {

    }

    void ApplyGravity()
    {
        bool grounded = false;
        RaycastHit hit;

        //grounded
        if(Physics.Raycast(transform.position, Vector3.down,out hit, footOffset, collisionTypes))
        {
            grounded = true;
            animationController.setFallLanding(grounded);
        }
        //underground
        float distance = Vector3.Distance(transform.position, hit.point);
        if (grounded && distance < footOffset) 
        {
            grounded = true;
            Vector3 desiredPos = transform.position;
            desiredPos.y += footOffset - distance;
            transform.position = desiredPos;
            animationController.setFallLanding(grounded);
        }


        //in air
        if (!grounded)
        {
            //Close to the ground (Used to fix jittering when running downhill)
            if (Physics.Raycast(transform.position, Vector3.down, out hit, footOffset * 1.3f, collisionTypes)) 
            {
                distance = Vector3.Distance(transform.position, hit.point);
                if(distance > footOffset)
                {
                    grounded = true;
                    animationController.setFallLanding(grounded);
                    Vector3 desiredPos = transform.position;
                    desiredPos.y -= distance - footOffset;
                    transform.position = desiredPos;
                }                
            }
            else//falling
            {
                float gravity = 9.8f;
                Vector3 position = transform.position;
                position.y -= gravity * Time.deltaTime;
                transform.position = position;
                animationController.setFallLanding(grounded);
            }
        }        
    }


    Vector3 BestMovement()
    {
        Vector3 movementDirection = input.GetDesiredMovement(transform);
        if (movementDirection != Vector3.zero)
        {
            Debug.DrawRay(transform.position, movementDirection, Color.green);


            //if Colision
            if (Physics.SphereCast(transform.position, CollisionWidth, movementDirection, out collisionHit, collisionDistance, collisionTypes, QueryTriggerInteraction.UseGlobal)) 
            {
                //check if can go up
                RaycastHit upLocation;
                Vector3 checkUp = Quaternion.LookRotation(movementDirection) * (Quaternion.AngleAxis(-maxClimbableAngle, Vector3.right) * Vector3.forward); ///
                Debug.DrawRay(transform.position, checkUp, Color.blue); ///
                if (!Physics.SphereCast(transform.position, CollisionWidth, checkUp, out upLocation, collisionDistance * 2, collisionTypes, QueryTriggerInteraction.UseGlobal)) 
                {
                    int i = 5;
                    checkUp = Quaternion.LookRotation(movementDirection) * (Quaternion.AngleAxis(-maxClimbableAngle + i, Vector3.right) * Vector3.forward); ///
                    while (!Physics.SphereCast(transform.position, CollisionWidth, checkUp, out upLocation, collisionDistance * 2, collisionTypes, QueryTriggerInteraction.UseGlobal))
                    {
                        i += 5;
                        checkUp = Quaternion.LookRotation(movementDirection) * (Quaternion.AngleAxis(-maxClimbableAngle + i, Vector3.right) * Vector3.forward); ; ///
                    }
                    return transform.InverseTransformDirection(checkUp * speed * Time.deltaTime);
                }
                else //cant go up
                {
                    //set collision bool
                    colliding = true;

                    //find end of colision left
                    Vector3 checkLeft = movementDirection;
                    RaycastHit lastHit;
                    while (Physics.SphereCast(transform.position, CollisionWidth, checkLeft, out lastHit, collisionDistance, collisionTypes, QueryTriggerInteraction.UseGlobal))
                    {
                        checkLeft = Quaternion.AngleAxis(-5f, Vector3.up) * checkLeft;
                    }

                    //find end of colision right
                    Vector3 checkRight = movementDirection;
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
        //visualise coll distance
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, collisionDistance);
        //visualise coll width
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CollisionWidth);
        //visualise foot pos
        Gizmos.color = Color.blue;
        Vector3 footpos = transform.position;
        footpos.y -= footOffset;
        Gizmos.DrawLine(transform.position, footpos);
    }
}
