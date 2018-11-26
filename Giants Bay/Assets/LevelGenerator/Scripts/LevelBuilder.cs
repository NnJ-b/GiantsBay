using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LevelBuilder : MonoBehaviour {

    public MapGenerator mapGenerator;

    public NavMeshGenerator navGenerator;
    public NavMeshSurface navMeshSurface;
    public FoliageBuilder foliageBuilder;

    public GameObject spawner;

    public FarmController farm;
    public HouseController house;
    public BuildSiteController buildSite;
    public TreasureChestController treasureChest;
    public GiantHouseController giantHouse;


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

        //creat the mesh //NEEDS TO BE after color map!
        mapGenerator.drawMode = MapGenerator.DrawMode.Mesh;
        mapGenerator.Generatemap();

        //creats the Foliage
        foliageBuilder.BuildFoliage();

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
                Vector3[] buildSiteLocations = new Vector3[SaveLoad.LoadInt("BuildSiteCount")];
                Vector3[] treasureChestLocations = new Vector3[SaveLoad.LoadInt("TreasureChestCount")];
                Vector3[] giantHouseLocations = new Vector3[SaveLoad.LoadInt("GiantHouseCount")];

                //Spawn Farms
                for (int i = 0; i < farmLocations.Length; i++)
                {
                    farmLocations[i] = SaveLoad.LoadLocation("Farm", i); //new Vector3(SaveLoad.LoadFloat("Farm" + i + "x"), SaveLoad.LoadFloat("Farm" + i + "y"), SaveLoad.LoadFloat("Farm" + i + "z"));
                    Instantiate(farm, farmLocations[i], Quaternion.identity);
                }

                //Spawn Homes
                for (int i = 0; i < homeLocations.Length; i++)
                {
                    homeLocations[i] = SaveLoad.LoadLocation("Home", i); //new Vector3(SaveLoad.LoadFloat("Home" + i + "x"), SaveLoad.LoadFloat("Home" + i + "y"), SaveLoad.LoadFloat("Home" + i + "z"));
                    Instantiate(house, homeLocations[i], Quaternion.identity);
                }

                //Spawn Buildsites
                for (int i = 0; i < buildSiteLocations.Length; i++)
                {
                    buildSiteLocations[i] = SaveLoad.LoadLocation("BuildSite", i);
                    Instantiate(buildSite, buildSiteLocations[i], Quaternion.identity);
                }

                //SpawnTreasureChest
                for (int i = 0; i < treasureChestLocations.Length; i++)
                {
                    treasureChestLocations[i] = SaveLoad.LoadLocation("TreasureChest", i);
                    TreasureChestController chest = Instantiate(treasureChest, treasureChestLocations[i], Quaternion.identity);
                    if(SaveLoad.LoadInt("TreasureChest" + i + "used") == 1)
                    {
                        chest.used = true;                       
                    }
                }

                //SpawnGiantCount
                for (int i = 0; i < giantHouseLocations.Length; i++)
                {
                    giantHouseLocations[i] = SaveLoad.LoadLocation("GiantHouse", i);
                    Instantiate(giantHouse, giantHouseLocations[i], Quaternion.identity);                 
                }
            }
        }       

        Destroy(this);
    }
}
