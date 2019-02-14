using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public Vector3 GetDesiredMovement(Transform player)
    {
        Quaternion rotation = Quaternion.Euler(0, player.eulerAngles.y, 0);
        Vector3 desiredMovement = rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        return desiredMovement;
    }

    public Vector2 MouseDelta(bool clickDependent)
    {
        if(clickDependent)
        {
            if(Input.GetMouseButton(0))
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
}
