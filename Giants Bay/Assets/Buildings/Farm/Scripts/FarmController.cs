using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmController : Interactable {

    public int boosters;
    public float productionRate;

    private void Start()
    {
        StartCoroutine("ProduceBoosters");
    }

    IEnumerable ProduceBoosters()
    {
        boosters++;
        yield return new WaitForSeconds(productionRate); 
    }




}
