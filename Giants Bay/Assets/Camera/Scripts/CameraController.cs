using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [Header("References")]
    public GameObject player;

    [Header("Camera Location")]
    public float zoom;
    public float maxZoom;
    public float minZoom;
    public float zoomSpeed;
    public Vector3 offset;
    [Range(0.0001f, 1)]
    public float camSmooth = 1f;

    [Header("Camera Rotation")]
    public bool allowCamRotation;
    [Range(.001f,2)]
    public float rotationSpeed = 2f;
    
	
	void Update () {        
        //check for mousewheel for zoom
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            zoom += zoomSpeed;
            ClampZoom();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            zoom -= zoomSpeed;
            ClampZoom();
        }       

        //rotate arround player
        if(allowCamRotation)
        {
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed * -1;
            Quaternion camTurnAngle = Quaternion.AngleAxis(rotation, Vector3.up);
            Ray ray = new Ray(player.transform.position + (camTurnAngle * offset) * zoom, Vector3.forward);
            float sphereRadius = 2f;
            if(Physics.SphereCast(ray, sphereRadius,Mathf.Infinity,null))
            offset = camTurnAngle * offset;
        }

        //maintain position with player
        Vector3 newPos = player.transform.position + offset * zoom;
        transform.position = Vector3.Slerp(transform.position, newPos, camSmooth);
        transform.LookAt(player.transform);

    }


    void ClampZoom()
    {
        //Check Camera Zoom and clamp if needs;
        if (zoom > maxZoom)
        {
            zoom = maxZoom;
        }
        else if (zoom < minZoom)
        {
            zoom = minZoom;
        }
    }
}
