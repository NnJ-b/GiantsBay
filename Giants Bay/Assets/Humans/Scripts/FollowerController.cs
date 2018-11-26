using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerController : HumanClass {

    private PlayerController player;
    public bool targeted = false;
    public GameObject noFarmWarnings;
    public GameObject selectAFarmWarning;
    public Transform assignedFarm;

    [Header("Movement")]
    public NavMeshAgent navMeshAgent;
    public float IdleDistance = 5f;
    public float IdleUpdateSpeed = 5f;
    CanvasController canvasController;

    public bool inFarm = false;



    public enum State {Idle, Follow, Scavange}
    public State state;

   
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        canvasController = player.canvas.GetComponent<CanvasController>();
        StartCoroutine("Wander");
	}

    public void updateNavMeshAgent(Vector3 destination)
    {
        float x = Random.Range(-2.5f, 2.5f);
        Vector3 variance = new Vector3(x, 0, x);
        navMeshAgent.SetDestination(destination + variance);
    }

    IEnumerator Wander()
    {
        while(true)
        {
            if(state == State.Idle)
            {
                float x = Random.Range(-IdleDistance, IdleDistance);
                Vector3 variance = new Vector3(x, 0, x);
                Vector3 destination = transform.position + variance;
                navMeshAgent.SetDestination(destination);
                Debug.Log("Destination: " + destination);
            }
            yield return new WaitForSeconds(IdleUpdateSpeed);
        }
    }

    public void Scavange()
    {
        if(Buildings.Farms.Count > 0)
        {
            CanvasController canvasController = player.canvas.GetComponent<CanvasController>();
            if (canvasController.showingMap == true)
            {
                //show select farm option
            }
            else
            {
                canvasController.TogellMap();
            }

            Instantiate(selectAFarmWarning, player.canvas.transform.position, Quaternion.identity, player.canvas.transform);
            canvasController.followerControll = true;
            canvasController.followerBeingControled = this;
        }
        else
        {
            Instantiate(noFarmWarnings,player.canvas.transform.position,Quaternion.identity,player.canvas.transform);
        }
    }

    public void MoveToAssignedFarm()
    {
        if(assignedFarm != null)
        {
            navMeshAgent.SetDestination(assignedFarm.transform.position);
            StartCoroutine(InteractWithAssignedFarm());
        }
    }

    public IEnumerator InteractWithAssignedFarm()
    {
        while (!inFarm)
        {
            if(Vector3.Distance(transform.position,assignedFarm.position) < assignedFarm.GetComponent<Buildings>().interactableRange *1.2)
            {
                inFarm = true;
                assignedFarm.GetComponent<FarmController>().Ocupants.Add(this);
                gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
