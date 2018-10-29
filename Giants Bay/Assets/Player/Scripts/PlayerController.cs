using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    private Camera cam;

    [Tooltip("Between 0-100")]
    private int health = 100;

    [Header("Ray Trace ")]
    public NavMeshAgent navMeshAgent;
    public float rayDistance = 500;
    public LayerMask Interactable;

    [Header("UI References")]
    public Slider healthBar;
    public Slider sizeBar;
    public TextMeshProUGUI BoooterCount;

    [Header("Spawn")]
    public float spawnOffset = 1f;

    [Header("Navigation")]
    public float speed;
    [SerializeField]
    public Interactable selected = null;
    public Interactable previouslySelected = null;
    public bool follow = false;
    public bool interacting;
    public float stopingDistanceInteractable;
    public float stopingdistanceEnemy;

    [Header("Combat")]
    public float baseDamage = 5f;
    public float damageAdder;
    private float damagePerHit;
    public float rangeMultiplyer = 1;

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
    [Range(10, 1000)]
    public int boosterEffect;
    [Tooltip("raises the asymptote")]
    public float boostMultiplyer = 1;


    private void Awake()
    {
        //settles (Temporary)
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, Mathf.Infinity, Interactable))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + spawnOffset, transform.position.z);
        }

        cam = Camera.main;


        LoadPlayerValues();

        if (rangeMultiplyer <1f)
        {
            rangeMultiplyer = 1f;
        }
        
        CalculateDamage();
        AddBoosters(0);
    }

    //MOUSEONLY!!!!!
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void Update ()
    {
        UpdateStopingDistance();
        Navigation();
        Attack();
        ChangeSize();
    }

    public void UpdateStopingDistance()
    {
        if(selected != null)
        {
            navMeshAgent.stoppingDistance = selected.interactableRange * rangeMultiplyer * .8f;
        }
    }

    private void LoadPlayerValues()
    {
        boosters = SaveLoad.LoadInt("Boosters");
        health = SaveLoad.LoadInt("Health");
        if(health == 0)
        {
            health = 100;
        }
    }

    public void AnimationState()
    {
        //close enought to attack?
        if (DistanceToEnemy() <= navMeshAgent.stoppingDistance*1.2f)
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
            //checks if over UI
            if(!IsPointerOverUIObject())
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
            }            

            //checks if selected changed
            if (selected != previouslySelected && previouslySelected != null)
            {
                previouslySelected.StopFocus();
            }
        }

        //selected an enemy?
        if (follow == true && selected !=null)
        {
            //checks to start animation
            AnimationState();
            //updates destination everyframe
            navMeshAgent.SetDestination(selected.transform.position);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
    }

    public void AddBoosters(int amount)
    {
        boosters += amount;
        if(boosters < 0)
        {
            boosters = 0;
        }
        BoooterCount.SetText(boosters.ToString());
        SaveLoad.SaveInt("Boosters", boosters);
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
                selected.CheckHealth();
            }
            attackReady = false;          
        }
    }

    public bool HitDetection()
    {
        //close enough to hit?
        if (DistanceToEnemy() <= navMeshAgent.stoppingDistance * rangeMultiplyer)
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
        healthBar.value = health;
        AddBoosters(-damageAmount);
        ChangeSize();
        SaveLoad.SaveInt("Health", health);
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
            return x;
        }
        else
        {
            return 0f;
        }
    }

    public void ChangeSize()
    {
        sizeBar.value = CalculateBoosterEffect();
        
        float x = 1 + (CalculateBoosterEffect() * boostMultiplyer);

        transform.localScale = Vector3.Slerp(transform.localScale, new Vector3(x, x, x), sizeLerpSpeed);
    }
}
