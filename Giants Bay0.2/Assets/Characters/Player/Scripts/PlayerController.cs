using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public PlayerMotor motor;
    public PlayerInventory inventory;
    public PlayerInteract interact;    
}
