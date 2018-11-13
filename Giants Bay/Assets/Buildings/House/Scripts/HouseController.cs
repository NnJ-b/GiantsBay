﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : Buildings {

    public static int Capacity = 1;
    public int CapacityPerHome = 5;

    public new void Awake()
    {
        structureType = Structure.House;
        NewBuilding(this);
        CalculateCapacity();
    }

    void CalculateCapacity()
    {
        Capacity += CapacityPerHome;
        Debug.Log(Capacity + ": Capacity");
        UpdateFollowersGUI();
    }

    public void UpdateFollowersGUI()
    {
        if (playerController == null)
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        playerController.capacity = Capacity;
        playerController.follwerPerCapacity.SetText(playerController.followers.Count.ToString() + "/" + Capacity);
    }



}
