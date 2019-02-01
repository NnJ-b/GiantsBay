using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple Inventory Scripts!!!");
        }
        instance = this;
    }
    #endregion

    public PlayerMotor motor;
    public PlayerInventory inventory;
    public PlayerInteract interact;    


}
