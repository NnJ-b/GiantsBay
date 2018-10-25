using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmController : Interactable {

    public int boosters;
    public float productionRate;

    private void OnEnable()
    {
        StartCoroutine("ProduceBoosters");
    }

    public override void Interact()
    {
        base.Interact();

    }

    IEnumerator ProduceBoosters()
    {
        while (true)
        {
            Debug.Log("StartedCoroutine");
            boosters++;
            yield return new WaitForSeconds(productionRate);
        }
    }

}
