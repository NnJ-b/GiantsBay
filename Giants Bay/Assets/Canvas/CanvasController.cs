using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    public GameObject infoPanel;
    public GameObject mapButton;
    public GameObject map;

    public MapGenerator mapGenerator;

    public Transform player;
    public RectTransform playerIcon;

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
        mimimapWidth = map.GetComponent<RectTransform>().rect.width;
        mimimapHeight = map.GetComponent<RectTransform>().rect.height;
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
            //Apllys Location
            playerIcon.anchoredPosition = new Vector3(Mathf.Lerp(mimimapWidth / -2, mimimapWidth / 2, x), Mathf.Lerp(mimimapHeight / -2, mimimapHeight / 2, z),1);

            //Rotation
            Vector3 playerIconRotation = playerIcon.transform.eulerAngles;
            playerIconRotation.z = player.eulerAngles.y;
            playerIcon.transform.eulerAngles = playerIconRotation;
        }    
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


}
