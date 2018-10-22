using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlerSpawner : MonoBehaviour {

    public MapGenerator mapGenerator;
    public float settlerCount;
    public GameObject settler;

    

	// Use this for initialization
	void Start () {
        for (int i = 0; i < settlerCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(mapGenerator.mapWidth * 10 / -2, mapGenerator.mapWidth * 10 / 2), 500f, Random.Range(mapGenerator.mapHeight * 10 / -2, mapGenerator.mapHeight * 10 / 2));
            GameObject instance = Instantiate(settler, position, Quaternion.identity);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
