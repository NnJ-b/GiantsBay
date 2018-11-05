﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerController))]
public class PlayerMotor : MonoBehaviour {

    private PlayerController controller;
    

    [Header("Ray Trace ")]
    public NavMeshAgent navMeshAgent;
    public float rayDistance = 500;
    public LayerMask Interactable;

    [Header("Navigation")]
    public float speed;
    [SerializeField]
    public Interactable selected = null;
    public Interactable previouslySelected = null;
    public bool follow = false;
    public bool interacting;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        navMeshAgent.speed = speed;
    }

    void Update ()
    {
        Navigation();
        Follow();
    }

    public void UpdateStopingDistance()
    {
        if (selected != null)
        {
            navMeshAgent.stoppingDistance = selected.interactableRange * controller.combat.rangeMultiplyer * .8f;
        }
    }
    public float DistanceToEnemy()
    {
        //how far is the enemy?
        if (follow == true && selected != null)
        {
            return Vector3.Distance(transform.position, selected.transform.position);
        }
        else
        {
            return 2000f;
        }
    }

    public void Navigation()
    {
        //raycast
        if (Input.GetMouseButtonDown(0))
        {
            //checks if over UI
            if (!controller.touch.IsPointerOverUIObject())
            {
                Debug.Log("pased test");
                Ray ray = controller.cam.ScreenPointToRay(Input.mousePosition);
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
                        UpdateStopingDistance();
                    }

                    if (hit.collider.tag == "Enemy")
                    {
                        follow = true;
                        interacting = false;
                        previouslySelected = selected;
                        selected = hit.transform.gameObject.GetComponent<Interactable>();
                        selected.Focus(this.controller);
                        UpdateStopingDistance();
                    }

                    if (hit.collider.tag == "Interactable")
                    {
                        follow = false;
                        interacting = true;
                        previouslySelected = selected;
                        selected = hit.transform.gameObject.GetComponent<Interactable>();
                        navMeshAgent.SetDestination(hit.transform.position);
                        selected.Focus(this.controller);
                        UpdateStopingDistance();

                    }
                }
            }

            //checks if selected changed
            if (selected != previouslySelected && previouslySelected != null)
            {
                previouslySelected.StopFocus();
            }
        }
    }

    public void Follow()
    {
        if(follow == true && selected != null)
        {
            navMeshAgent.SetDestination(selected.transform.position);
        }
    }
}
