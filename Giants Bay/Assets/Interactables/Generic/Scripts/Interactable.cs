using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public bool focused;

    public PlayerController playerController;

    public virtual void Interact()
    {
        Debug.Log("Interacted with" + transform.name);
    }

    public void Update()
    {
        if(playerController != null)
        {
            if(Vector3.Distance(transform.position, playerController.transform.position) <= playerController.navMeshAgent.stoppingDistance * 1.2f)
            {
                Interact();
            }
        }
    }


    public void Focus(PlayerController controller)
    {
        focused = true;
        playerController = controller;
    }

    public void StopFocus()
    {
        focused = false;
    }

}
