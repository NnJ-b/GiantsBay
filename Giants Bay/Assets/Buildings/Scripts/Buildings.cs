using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : Interactable {

    public enum Structure {Farm,House}
    public Structure structureType;

    public static List<Buildings> Farms = new List<Buildings>();
    public static List<Buildings> Homes = new List<Buildings>();

    public void NewBuilding(Buildings building)
    {
        if(building.structureType == Structure.Farm)
        {
            Farms.Add(this);
            SaveLoad.SaveInt("FarmsCount", Farms.Count);
            for (int i = 0; i < Farms.Count; i++)
            {
                SaveLoad.SaveLocation("Farm", Farms[i].transform, i);
            }

        }
        if(building.structureType == Structure.House)
        {
            Homes.Add(this);
            Debug.Log("Homes: " + Homes.Count);
            SaveLoad.SaveInt("HomesCount", Homes.Count);
            for (int i = 0; i < Homes.Count; i++)
            {
                SaveLoad.SaveLocation("Home", Homes[i].transform, i);
                /*SaveLoad.SaveFloat("Home" + i + "x", Homes[i].transform.position.x);
                SaveLoad.SaveFloat("Home" + i + "y", Homes[i].transform.position.y);
                SaveLoad.SaveFloat("Home" + i + "z", Homes[i].transform.position.z);*/
            }
        }
    }



}
