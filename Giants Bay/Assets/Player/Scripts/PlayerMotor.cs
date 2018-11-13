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
                        updateFollowersDestination(hit.point);
                    }

                    if (hit.collider.tag == "Enemy")
                    {
                        follow = true;
                        interacting = false;
                        previouslySelected = selected;
                        selected = hit.transform.gameObject.GetComponent<Interactable>();
                        selected.Focus(this.controller);
                        UpdateStopingDistance();
                        updateFollowersDestination(hit.point);
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
                        updateFollowersDestination(hit.point);
                    }
                    if(hit.collider.tag == "Human")
                    {
                        //check state and switch to other
                        FollowerController.State state = hit.transform.GetComponent<FollowerController>().state;
                        if (state == FollowerController.State.Follow)
                        {
                            hit.transform.GetComponent<FollowerController>().state = FollowerController.State.Idle;
                        }
                        else if (state == FollowerController.State.Idle)
                        {
                            hit.transform.GetComponent<FollowerController>().state = FollowerController.State.Follow;
                        }
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

    public void updateFollowersDestination(Vector3 destination)
    {
        //updates followers Destination
        for (int i = 0; i < controller.followers.Count; i++)
        {
            if (controller.followers[i].state == FollowerController.State.Follow)
            {
                controller.followers[i].updateNavMeshAgent(destination);
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
