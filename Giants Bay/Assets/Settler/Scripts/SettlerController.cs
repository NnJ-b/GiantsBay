﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlerController : MonoBehaviour
{

    public LayerMask walkable;
    public ObjectsToSpawn[] objectsToSpawn;
    public SettlerSpawner spawner;

    void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<SettlerSpawner>();
        Settle();
    }

    public void Settle()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, walkable))
        {
            Vector3 hitPoint = new Vector3(transform.position.x, hit.point.y, transform.position.z);

            List<ObjectsToSpawn> inRange = new List<ObjectsToSpawn>();


            for (int i = 0; i < objectsToSpawn.Length; i++)
            {
                if (hitPoint.y > objectsToSpawn[i].minHeightSpawnable)
                {
                    if (hitPoint.y < objectsToSpawn[i].maxHeightSpawnable)
                    {
                        inRange.Add(objectsToSpawn[i]);
                    }
                }
            }

            if (inRange.Count > 0)
            {
                //get Randome Object in range
                ObjectsToSpawn toSpawn = inRange[Random.Range(0, inRange.Count)];
                //settle
                Instantiate(toSpawn.prefab, new Vector3(hitPoint.x, hitPoint.y + toSpawn.spawnOffset, hitPoint.z), Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                spawner.Spawn();
                Destroy(this.gameObject);
            }           
        }
        else
        {
            spawner.Spawn();
            Destroy(this.gameObject);
        }
    }



    [System.Serializable]
    public struct ObjectsToSpawn
    {
        public string nameInspectorOnly;
        public GameObject prefab;
        public float spawnOffset;
        public float minHeightSpawnable;
        public float maxHeightSpawnable;
    }
}

