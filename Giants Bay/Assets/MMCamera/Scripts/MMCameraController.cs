using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMCameraController : MonoBehaviour {

    public float rotationSpeed;
    public Camera camera;
	
	// Update is called once per frame
	void Update () {
        camera.transform.LookAt(transform);
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
	}
}
