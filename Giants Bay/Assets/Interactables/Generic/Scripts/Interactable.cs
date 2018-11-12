using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public bool focused;
    public float health;

    public PlayerController playerController;

    public float interactableRange;

    public void Awake()
    {
        //to make sure interactable objects dont die
        if(health == 0)
        {
            health = 1f;
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with" + transform.name);
    }

    public virtual void Update()
    {
        if(playerController != null)
        {
            if(Vector3.Distance(transform.position, playerController.transform.position) <= playerController.motor.navMeshAgent.stoppingDistance*1.2f)
            {
                Interact();
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, interactableRange);
    }

    public void TakeDamage(float damageAmount)
    {
        health = health - damageAmount;
        Debug.Log(health + ": " + name);
        CheckHealth();

    }

    public void CheckHealth()
    {
        if(health <= 0f)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("Die: " + name);
        Destroy(gameObject);
    }

    public void Focus(PlayerController controller)
    {
        focused = true;
        playerController = controller;
    }

    public virtual void StopFocus()
    {
        focused = false;
    }

}
