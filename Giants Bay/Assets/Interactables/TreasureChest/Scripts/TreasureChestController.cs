using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestController : Interactable {

    public int treasureMax = 10;
    public int treasureMin = 1;
    private bool used;

    public override void Interact()
    {
        base.Interact();
        AddToMap();
        if(!used)
        {
            GiveTreasure();
        }
    }

    public void AddToMap()
    {

    }

    public void GiveTreasure()
    {
        int treasureAmount = Random.Range(treasureMin,treasureMax);

        if(playerController != null)
        {
            playerController.AddBoosters(treasureAmount);
            used = true;
        }
    }

}
