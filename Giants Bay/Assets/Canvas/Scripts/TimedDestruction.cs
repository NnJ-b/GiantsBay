using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestruction : MonoBehaviour {

    public float Time;

	void Start () {
        StartCoroutine(Destruction(Time));
	}

    private IEnumerator Destruction(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
	
}
