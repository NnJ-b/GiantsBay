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

    public override void Update()
    {
        base.Update();
        if (selected != null && master.GoingHome == false)
        {
            navMeshAgent.SetDestination(selected.transform.position);
        }
        else
        {
            navMeshAgent.SetDestination(master.home.transform.position);
        }
    }
    
    public void SelectTarget()
    {
        if (player.followers.Count > 0)
        {
            for (int i = 0; i < player.followers.Count; i++)
            {
                if(player.followers[i].targeted == false)
                {
                    selected = player.followers[i].gameObject;
                    return;
                }
            }
            selected = player.gameObject;
        }
        else
        {
            selected = player.gameObject;
        }
    }

    public void FreeSlaves()
    {
        FollowerController followerController = GetComponent<FollowerController>();
        followerController.enabled = true;
        followerController.gameObject.tag = "Human";
        player.followers.Add(followerController);
        player.updateFollowerGUI();
        this.enabled = false;
    }
}
