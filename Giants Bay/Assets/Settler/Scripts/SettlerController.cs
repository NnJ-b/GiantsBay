using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlerController : MonoBehaviour
{

    public LayerMask walkable;
    public ObjectsToSpawn[] objectsToSpawn;
    public SettlerSpawner spawner;

    public float maxDistanceToAnotherInteractable = 10f;

    public static List<GameObject> instances = new List<GameObject>();

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

            if(instances.Count > 0)
            {
                for (int i = 0; i < instances.Count; i++)
                {
                    float distance = Vector3.Distance(hitPoint, instances[i].transform.position);
                    if(distance < maxDistanceToAnotherInteractable)
                    {
                        spawner.Spawn();
                        Destroy(this.gameObject);
                        return;
                    }
                }
            }

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
                //get Random Object in range
                ObjectsToSpawn toSpawn = inRange[Random.Range(0, inRange.Count)];
                //settle
                GameObject instance = Instantiate(toSpawn.prefab, new Vector3(hitPoint.x, hitPoint.y + toSpawn.spawnOffset, hitPoint.z), Quaternion.identity);
                instances.Add(instance);
                Debug.Log(instances.Count);
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

