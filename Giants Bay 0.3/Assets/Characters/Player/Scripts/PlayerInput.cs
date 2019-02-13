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
}
