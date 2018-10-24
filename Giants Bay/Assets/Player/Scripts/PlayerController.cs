using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    private Camera cam;

    [Tooltip("Between 0-100")]
    private int health = 100;

    [Header("Ray Trace ")]
    public NavMeshAgent navMeshAgent;
    public float rayDistance = 500;
    public LayerMask Interactable;

    [Header("Spawn")]
    public float spawnOffset = 1f;

    [Header("Navigation")]
    public float speed;
    public Interactable selected = null;
    public Interactable previouslySelected = null;
    public bool follow;
    public bool interacting;
    public float stopingDistanceInteractable;
    public float stopingdistanceEnemy;

    [Header("Combat")]
    public float baseDamage = 5f;
    public float damageAdder;
    private float damagePerHit;

    [Header("Animation Controlls")]
    public Animator animator;
    [Range(.001f,1)]
    public float sizeLerpSpeed;
    public bool attacking = false;
    public bool endAttack = false;
    public bool startAttack = false;
    public bool attackReady = false;

    [Header("Colectables")]
    public int boosters;
    [Tooltip("The Higher the value to longer it will take to reach the asymptote")]
    [Range(10, 150)]
    public int boosterEffect;
    [Tooltip("raises the asymptote")]
    public float boostMultiplyer = 1;


    void Start ()
    {
        cam = Camera.main;
	}

    private void Awake()
    {
        //settles (Temporary)
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, Mathf.Infinity, Interactable))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y+spawnOffset, transform.position.z);
        }

        CalculateDamage();
    }

    void Update ()
    {
        Navigation();
        Attack();
        ChangeSize();
    }

    public void AnimationState()
    {
        //close enought to attack?
        if (DistanceToEnemy() <= navMeshAgent.stoppingDistance * 1.2f)
        {
            animator.SetBool("Attacking", true);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
    }

    public void Navigation()
    {
        //raycast
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance, Interactable))
            {
                if (hit.collider.tag == "Ground")
                {
                    navMeshAgent.stoppingDistance = 0f;
                    navMeshAgent.SetDestination(hit.point);
                    follow = false;
                    interacting = false;
                    previouslySelected = selected;
                    selected = null;
                }
                if (hit.collider.tag == "Enemy")
                {
                    follow = true;
                    interacting = false;
                    previouslySelected = selected;
                    selected = hit.transform.gameObject.GetComponent<Interactable>();
                    navMeshAgent.stoppingDistance = stopingdistanceEnemy;

                    selected.Focus(this);
                }
                if (hit.collider.tag == "Interactable")
                {
                    follow = false;
                    interacting = true;
                    navMeshAgent.stoppingDistance = stopingDistanceInteractable;
                    previouslySelected = selected;
                    selected = hit.transform.gameObject.GetComponent<Interactable>();
                    navMeshAgent.SetDestination(hit.point);

                    selected.Focus(this);
                }
            }

            //checks if selected changed
            if (selected != previouslySelected && previouslySelected != null)
            {
                previouslySelected.StopFocus();
            }
        }

        //selected an enemy?
        if (follow == true)
        {
            //checks to start animation
            AnimationState();
            //updates destination everyframe
            navMeshAgent.SetDestination(selected.transform.position);
        }
    }

    private void Attack()
    {
        //stop moving
        if (attacking)
        {
            navMeshAgent.speed = 0f;
        }
        //start Moving
        else
        {
            navMeshAgent.speed = speed;
        }

        //Gets Attack Ready
        if (startAttack)
        {
            attackReady = true;
        }

        //Attacks
        if (endAttack && attackReady)
        {
            //check Distance after animation
            if (HitDetection())
            {
                //aplly Damage
                selected.TakeDamage(damagePerHit);
                Debug.Log("hit" + selected.name);
            }
            attackReady = false;          
        }
    }

    public bool HitDetection()
    {
        //close enough to hit?
        if (DistanceToEnemy() <= navMeshAgent.stoppingDistance * 1.2f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float DistanceToEnemy()
    {
        //how far is the enemy?
        if (selected != null)
        {
            return Vector3.Distance(transform.position, selected.transform.position);
        }
        else
        {
            return 2000f;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        //ouch
        health = health - damageAmount;
        Debug.Log(health);
    }   

    public void CalculateDamage()
    {
        damagePerHit = baseDamage + damageAdder;
    }

    public float CalculateBoosterEffect()
    {
        if(boosters > 0f)
        {
            float y = boosters + boosterEffect;
            float x = (boosters / y);
            float z = x * boostMultiplyer;
            return z;
        }
        else
        {
            return 0f;
        }
    }

    public void ChangeSize()
    {
        float x = 1 + CalculateBoosterEffect();

        transform.localScale = Vector3.Slerp(transform.localScale, new Vector3(x, x, x), sizeLerpSpeed);
    }
}
