using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    private float camZoom = 1f;


    public Vector3 GetDesiredMovement(Transform player)
    {
        Quaternion rotation = Quaternion.Euler(0, player.eulerAngles.y, 0);
        Vector3 desiredMovement = rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        return desiredMovement;
    }

    public Vector2 MouseDeltaPerFrame(bool trigerDependent, bool triger)
    {
        if(trigerDependent)
        {
            if(triger)
            {
                return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            }
            else
            {
                return Vector2.zero;
            }
        }
        else
        {
            return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }

    public float cameraZoom()
    {
        camZoom += Input.GetAxis("Mouse ScrollWheel");
        if(camZoom <.6f)
        {
            camZoom = .6f;
        }
        if(camZoom > 3f)
        {
            camZoom = 3f;
        }
        return camZoom;

    }
}
