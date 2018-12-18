using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted With: " + transform.name);
    }
}
