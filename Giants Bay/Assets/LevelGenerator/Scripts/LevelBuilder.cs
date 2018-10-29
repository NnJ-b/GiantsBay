using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LevelBuilder : MonoBehaviour {

    public MapGenerator mapGenerator;

    public NavMeshGenerator navGenerator;
    public NavMeshSurface navMeshSurface;

    public GameObject spawner;

    public FarmController farm;
    public HouseController house;


    private void Awake()
    {
        //update map info
        mapGenerator.seed = SaveLoad.LoadInt("Seed");
        mapGenerator.mapWidth = SaveLoad.LoadInt("MapWidth");
        mapGenerator.mapHeight = SaveLoad.LoadInt("MapHeight");


        //makes sure there is a value (will crash without!)
        if (mapGenerator.mapWidth == 0)
        {
            mapGenerator.mapWidth = 100;
            mapGenerator.mapHeight = 100;
        }

        //creat the FalloffMap
        mapGenerator.falloffMap = FalloffGenerator.GenerateFalloffMap(mapGenerator.mapWidth, mapGenerator.mapHeight);


        //Creat The ColorMap
        mapGenerator.drawMode = MapGenerator.DrawMode.ColorMap;
        mapGenerator.Generatemap();

        //creat the mesh NEEDS TO BE after color map!
        mapGenerator.drawMode = MapGenerator.DrawMode.Mesh;
        mapGenerator.Generatemap();

        if(SceneManager.GetActiveScene().name != "MainMenu")
        {
            navGenerator.BuildNavMesh(navMeshSurface);
            if (SaveLoad.LoadInt("NewGame") == 1)
            {
                Instantiate(spawner, Vector3.zero, Quaternion.identity);
                SaveLoad.SaveInt("NewGame", 0);
            }
            else
            {
                //spawn Saved Items
                Vector3[] farmLocations = new Vector3[SaveLoad.LoadInt("FarmsCount")];
                Vector3[] homeLocations = new Vector3[SaveLoad.LoadInt("HomesCount")];


                for (int i = 0; i < farmLocations.Length; i++)
                {
                    farmLocations[i] = new Vector3(SaveLoad.LoadFloat("Farm" + i + "x"), SaveLoad.LoadFloat("Farm" + i + "y"), SaveLoad.LoadFloat("Farm" + i + "z"));
                }
                for (int i = 0; i < farmLocations.Length; i++)
                {
                    Instantiate(farm, farmLocations[i], Quaternion.identity);
                }


                for (int i = 0; i < homeLocations.Length; i++)
                {
                    homeLocations[i] = new Vector3(SaveLoad.LoadFloat("Home" + i + "x"), SaveLoad.LoadFloat("Home" + i + "y"), SaveLoad.LoadFloat("Home" + i + "z"));
                }
                for (int i = 0; i < homeLocations.Length; i++)
                {
                    Instantiate(house, homeLocations[i], Quaternion.identity);
                }
            }
        }       

        Destroy(this);
    }
}
