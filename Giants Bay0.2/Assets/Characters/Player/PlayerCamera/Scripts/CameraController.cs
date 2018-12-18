using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public GameObject player;


    [Header("Camera Location")]
    public float zoom;
    public Vector2 zoomLimits;
    public float zoomSpeed;
    public Vector3 offset;   
    [Range(0.0001f, 1)]
    public float camSmooth = 1f;

    //mouseRotation
    public Transform cameraParent;
    Vector3 mouseStart = Vector3.zero;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
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

        //maintain position with player
        Vector3 newPos = player.transform.position + new Vector3(offset.x, offset.y, offset.z) * zoom;
        transform.position = Vector3.Slerp(transform.position, newPos, camSmooth);
        transform.LookAt(player.transform);
    }


    void ClampZoom()
    {
        //Check Camera Zoom and clamp if needs;
        if (zoom > zoomLimits.y)
        {
            zoom = zoomLimits.y;
        }
        else if (zoom < zoomLimits.x)
        {
            zoom = zoomLimits.x;
        }
    }    
}
