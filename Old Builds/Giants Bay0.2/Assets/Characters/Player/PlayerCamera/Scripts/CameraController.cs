using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public GameObject player;


    [Header("Camera Location")]
    private float YRotation;
    public Vector3 defaultLocation;
    public float zoom;
    public Vector2 zoomLimits;
    public Vector2 rotationLimits;
    public float zoomSpeed;

    private void Start()
    {
        transform.localPosition = defaultLocation * zoom;
    }

    void FixedUpdate()
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
        
        if(Input.GetMouseButton(0))
        {
            YRotation -= Input.GetAxis("Mouse Y");
        }

        ClampRotation();        

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

        transform.localPosition = defaultLocation * zoom;
    }    

    void ClampRotation()
    {
        YRotation = Mathf.Clamp(YRotation, rotationLimits.x, rotationLimits.y);
        player.transform.localEulerAngles= new Vector3(YRotation, 0, 0);        
    }
}
