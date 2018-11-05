using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlaveController : HumanClass {
    [Header("References")]
    public GiantController master;
    public PlayerController player;
    public NavMeshAgent navMeshAgent;
    private GameObject selected;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        SelectTarget();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(selected != null)
        {
            navMeshAgent.SetDestination(selected.transform.position);
        }
	}

    public void SelectTarget()
    {
        if (player.followers.Count > 0)
        {
            int i = Random.Range(0, player.followers.Count);
            selected = player.followers[i].gameObject;
        }
        else
        {
            selected = player.gameObject;
        }
    }

    public void FreeSlaves()
    {
        GetComponent<FollowerController>().enabled = true;
        this.enabled = false;
    }
}
