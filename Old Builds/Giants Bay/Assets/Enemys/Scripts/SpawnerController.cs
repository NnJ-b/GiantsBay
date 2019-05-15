using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {

    public GameObject TempChestPrefab;
    public GameObject TempSitePrefab;
    public GameObject TempHumanPrefab;
    


    private GameObject player;
    
    [SerializeField]
    public SpawnableObjects[] objectsToSpawn;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SpawnRandom()
    {
        int i;
        i = Random.Range(0, objectsToSpawn.Length);

        Instantiate(objectsToSpawn[i].prefab, Vector3.zero, Quaternion.identity);        
    }

    #region temporary class's

    public void SpawnChest()
    {
        Instantiate(TempChestPrefab, new Vector3(0,.5f,0), Quaternion.identity); 
    }

    public void SpawnSite()
    {
        Instantiate(TempSitePrefab, new Vector3(0, .5f, 0), Quaternion.identity);
    }

    public void SpawnHuman()
    {
        Instantiate(TempHumanPrefab, new Vector3(0, 1, 0), Quaternion.identity);
    }





    #endregion

    [System.Serializable]
    public struct SpawnableObjects
    {
        public string name;
        public GameObject prefab;
    }
}
