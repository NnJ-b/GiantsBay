using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : Interactable {

    public enum Structure {Farm,House}
    public Structure structureType;

    public static List<Buildings> Farms = new List<Buildings>();
    public static List<Buildings> Homes = new List<Buildings>();

    public GameObject icon;


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
            //save
            Homes.Add(this);
            Debug.Log("Homes: " + Homes.Count);
            SaveLoad.SaveInt("HomesCount", Homes.Count);
            for (int i = 0; i < Homes.Count; i++)
            {
                SaveLoad.SaveLocation("Home", Homes[i].transform, i);                
            }
        }
        
    }



}
