﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    public GameObject infoPanel;
    public GameObject mapButton;
    public GameObject map;
    public GameObject mapImage;

    public MapGenerator mapGenerator;

    public Transform player;
    public RectTransform playerIcon;

    public BuildSiteController buildSiteController;

    public bool showingMap = false;


    public float mimimapWidth;
    public float mimimapHeight;

    public void Start()
    {
        HideAll();
        ActivateInGame();
        mimimapWidth = map.GetComponent<RectTransform>().rect.width;
        mimimapHeight = map.GetComponent<RectTransform>().rect.height;
    }

    public void Update()
    {
        mimimapWidth = mapImage.GetComponent<RectTransform>().rect.width;
        mimimapHeight = mapImage.GetComponent<RectTransform>().rect.height;
        PlayerMapLocation();
    }

    public void PlayerMapLocation()
    {    
        if(showingMap)
        {
            //Calculates Location
            float x = player.position.x;
            float z = player.position.z;
            x = Mathf.InverseLerp((mapGenerator.mapWidth * 10) / -2, (mapGenerator.mapWidth * 10) / 2, x);
            z = Mathf.InverseLerp((mapGenerator.mapHeight * 10) / -2, (mapGenerator.mapHeight * 10) / 2, z);
            //Apply Location
            playerIcon.anchoredPosition = new Vector3(Mathf.Lerp(mimimapWidth / -2, mimimapWidth / 2, x), Mathf.Lerp(mimimapHeight / -2, mimimapHeight / 2, z),1);

            //Rotation
            playerIcon.rotation = Quaternion.Euler(0f, 0f, player.eulerAngles.y);
            
        }    
    }

    public void MMnewBuilding(GameObject Prefab, Transform location)
    {
        //Calculates Location
        float x = location.position.x;
        float z = location.position.z;
        x = Mathf.InverseLerp((mapGenerator.mapWidth * 10) / -2, (mapGenerator.mapWidth * 10) / 2, x);
        z = Mathf.InverseLerp((mapGenerator.mapHeight * 10) / -2, (mapGenerator.mapHeight * 10) / 2, z);
        //Apply Location
        Vector3 rectrans = new Vector3(Mathf.Lerp(mimimapWidth / -2, mimimapWidth / 2, x), Mathf.Lerp(mimimapHeight / -2, mimimapHeight / 2, z), 1);

        //GameObject icon = 
        GameObject instance = Instantiate(Prefab, rectrans, Quaternion.identity,map.transform);
        instance.GetComponent<RectTransform>().anchoredPosition = rectrans;

    }

    public void ActivateInGame()
    {
        infoPanel.SetActive(true);
        mapButton.SetActive(true);
    }

    public void TogellMap()
    {
        if(map.activeSelf)
        {
            map.SetActive(false);
            showingMap = false;
        }
        else
        {
            map.SetActive(true);
            showingMap = true;
        }
    }

    public void HideAll()
    {
        infoPanel.SetActive(false);
        mapButton.SetActive(false);
        map.SetActive(false);
    }



    //button Controlls
    public void SpawnFarm()
    {
        buildSiteController.SpawnFarm();
    }
    public void SpawnHouse()
    {
        buildSiteController.SpawnHouse();
    }

    public void FollowerStateFollow()
    {
        player.GetComponent<PlayerMotor>().SetFollowerState(FollowerController.State.Follow);
    }

    public void FollowerStateIdle()
    {
        player.GetComponent<PlayerMotor>().SetFollowerState(FollowerController.State.Idle);

    }

    public void FollowerStateScavange()
    {
        player.GetComponent<PlayerMotor>().SetFollowerState(FollowerController.State.Scavange);

    }
}
