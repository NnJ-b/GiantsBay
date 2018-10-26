using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : Interactable {

    public static int Capacity = 1;
    public static List<HouseController> Homes = new List<HouseController>();
    public int CapacityPerHome = 5;

    public new void Awake()
    {
        Homes.Add(this);
        CalculateCapacity();
    }

    void CalculateCapacity()
    {
        Capacity += CapacityPerHome;
        Debug.Log(Capacity + ": Capacity");            
    }



}
