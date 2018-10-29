using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmController : Buildings {

    public int boosters;
    public float productionRate = 60f;
    [Range(0, 1)]
    public float productionModifyer = 1f;

    

    private new void Awake()
    {
        structureType = Structure.Farm;
        NewBuilding(this);
        StartCoroutine("ProduceBoosters");
    }

    public override void Interact()
    {
        base.Interact();
        playerController.AddBoosters(boosters);
        boosters = 0;
    }

    IEnumerator ProduceBoosters()
    {
        while (true)
        {
            Debug.Log("StartedCoroutine");
            boosters++;
            yield return new WaitForSeconds(productionRate*productionModifyer);
        }
    }

}
