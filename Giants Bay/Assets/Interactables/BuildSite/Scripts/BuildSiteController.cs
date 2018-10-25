using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSiteController : Interactable {

    public GameObject farmPreFab;

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Override!!");

        //temporary Spawning Should allow to chose
        Instantiate(farmPreFab,transform.position,Quaternion.identity);


        Destroy(gameObject);
    }

}
