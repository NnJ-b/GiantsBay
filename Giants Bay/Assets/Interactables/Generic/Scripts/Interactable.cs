using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public bool focused;

    public PlayerController player;

    public virtual void Interact()
    {
        Debug.Log("Interacted with" + transform.name);
    }

    public void Update()
    {
        if(player != null)
        {
            if(Vector3.Distance(transform.position, player.transform.position) < player.navMeshAgent.stoppingDistance)
            {
                Interact();
            }
        }
    }


    public void Focus(PlayerController _player)
    {
        focused = true;
        player = _player;
    }

    public void StopFocus()
    {
        focused = false;
    }

}
