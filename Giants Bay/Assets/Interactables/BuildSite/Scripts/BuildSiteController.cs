using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSiteController : Interactable {

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Override!!");
    }

}
