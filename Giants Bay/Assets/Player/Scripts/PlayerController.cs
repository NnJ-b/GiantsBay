using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    private Camera cam;

    private int health = 100;

    [Header("Ray Trace ")]
    public NavMeshAgent navMeshAgent;
    public float rayDistance = 500;
    public LayerMask Interactable;

    [Header("Spawn")]
    public float spawnOffset = 1f;

    [Header("Navigation")]
    public Interactable selected = null;
    public Interactable previouslySelected = null;
    public bool attacking;
    public bool interacting;
    public float stopingDistanceInteractable;
    public float stopingdistanceEnemy;


    void Start ()
    {
        cam = Camera.main;
	}

    private void Awake()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, Mathf.Infinity, Interactable))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y+spawnOffset, transform.position.z);
        }
    }

    void Update ()
    {
        //raycast
		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance, Interactable))
            {
                if(hit.collider.tag == "Ground")
                {
                    navMeshAgent.stoppingDistance = 0f;
                    navMeshAgent.SetDestination(hit.point);
                    attacking = false;
                    interacting = false;
                    previouslySelected = selected;
                    selected = null;
                }
                if(hit.collider.tag == "Enemy")
                {
                    attacking = true;
                    interacting = false;
                    previouslySelected = selected;
                    selected = hit.transform.gameObject.GetComponent<Interactable>();
                    navMeshAgent.stoppingDistance = stopingdistanceEnemy;

                    selected.Focus(this);
                }
                if (hit.collider.tag == "Interactable")
                {
                    attacking = false;
                    interacting = true;
                    navMeshAgent.stoppingDistance = stopingDistanceInteractable;
                    previouslySelected = selected;
                    selected = hit.transform.gameObject.GetComponent<Interactable>();
                    navMeshAgent.SetDestination(hit.point);

                    selected.Focus(this);
                }
            }


            if (selected != previouslySelected && previouslySelected != null)
            {
                previouslySelected.StopFocus();
            }            
        }

        if (attacking == true)
        {
            navMeshAgent.SetDestination(selected.transform.position);
        }


    }

    public void TakeDamage(int damageAmount)
    {
        health = health - damageAmount;
        Debug.Log(health);
    }   
}
