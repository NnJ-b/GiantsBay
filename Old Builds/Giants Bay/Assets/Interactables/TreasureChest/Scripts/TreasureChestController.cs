using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestController : Interactable {

    public int treasureMax = 10;
    public int treasureMin = 1;
    public bool used;

    public static List<TreasureChestController> TreasureChests = new List<TreasureChestController>();


    private void Start()
    {
        TreasureChests.Add(this);
        SaveLoad.SaveInt("TreasureChestCount", TreasureChests.Count);
        for (int i = 0; i < TreasureChests.Count; i++)
        {
            SaveLoad.SaveLocation("TreasureChest", TreasureChests[i].transform, i);
        }
    }

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
            playerController.inventory.AddBoosters(treasureAmount);
            used = true;
            for (int i = 0; i < TreasureChests.Count; i++)
            {
                if(TreasureChests[i].used == true)
                {
                    SaveLoad.SaveInt("TreasureChest" + i + "used", 1);
                }
                else
                {
                    SaveLoad.SaveInt("TreasureChest" + i + "used", 0);
                }
            }
        }
    }

}
