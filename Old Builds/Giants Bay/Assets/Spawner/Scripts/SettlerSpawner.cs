using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlerSpawner : MonoBehaviour {

    public MapGenerator mapGenerator;
    public float settlerCount;
    public GameObject settler;

    

	void Start ()
    {
        mapGenerator = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();

        for (int i = 0; i < settlerCount; i++)
        {
            Spawn();
        }

    }


    public void Spawn()
    {
        Vector3 position = new Vector3(Random.Range(mapGenerator.mapWidth * 10 / -2, mapGenerator.mapWidth * 10 / 2), 1000, Random.Range(mapGenerator.mapHeight * 10 / -2, mapGenerator.mapHeight * 10 / 2));
        Instantiate(settler, position, Quaternion.identity);
    }
}
