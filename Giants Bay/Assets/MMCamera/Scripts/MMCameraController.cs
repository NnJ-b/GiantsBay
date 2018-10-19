using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMCameraController : MonoBehaviour {

    public float rotationSpeed;
    public Camera cam;
    public float zoomSpeed;
    public float zoomDistance;

    //small: 800
    //Medium: 1000
    //Large: 1500

	
	// Update is called once per frame
	void Update () {
        cam.transform.LookAt(transform);
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        float y = Mathf.Lerp(cam.transform.position.y, zoomDistance, zoomSpeed);
        cam.transform.position = new Vector3(cam.transform.position.x, y, cam.transform.position.z);
    }

    public void ToSmall()
    {
        zoomDistance = 800f;
    }

    public void ToMedium()
    {
        zoomDistance = 1000f;
    }

    public void ToLarge()
    {
        zoomDistance = 1500f;
    }
}
