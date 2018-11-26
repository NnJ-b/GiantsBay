using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageBuilder : MonoBehaviour {

    public LayerMask terrain;
    public MapGenerator mapGenerator;

    public float radius = 25;
    public int attempts = 2;
    public Vector2 regionSize = Vector2.one;

    public ObjectsToSpawn[] objectsToSpawn;

    List<Vector2> points = new List<Vector2>();
    List<Vector3> raycastPoints = new List<Vector3>();

    public void BuildFoliage()
    {
        raycastPoints.Clear();
        regionSize = new Vector2(mapGenerator.mapWidth*10, mapGenerator.mapHeight*10);
        points = DiscSamples.GeneratePoints(radius, regionSize, attempts);
        FindTerrain();
        PlacePrefab();
    }

    public void FindTerrain()
    {
        foreach (Vector2 point in points)
        {
            Ray ray = new Ray(new Vector3(point.x-(regionSize.x/2), 99f, point.y- (regionSize.y / 2)), Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrain))
            {
                raycastPoints.Add(hit.point);
            }
        }
    }

    public void PlacePrefab()
    {
        foreach (Vector3 point in raycastPoints)
        {
            List<ObjectsToSpawn> inRange = new List<ObjectsToSpawn>();
            for (int i = 0; i < objectsToSpawn.Length; i++)
            {
                if (point.y > objectsToSpawn[i].minHeightSpawnable)
                {
                    if (point.y < objectsToSpawn[i].maxHeightSpawnable)
                    {
                        inRange.Add(objectsToSpawn[i]);
                    }
                }
            }

            if(inRange.Count > 0)
            {
                ObjectsToSpawn toSpawn = inRange[Random.Range(0, inRange.Count)];
                Instantiate(toSpawn.prefab, point, Quaternion.identity);
            }
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
