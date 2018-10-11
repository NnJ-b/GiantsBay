using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    public int depth;
    public int width = 256;
    public int height = 256;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public float scale = 20f;


    private void Start()
    {
        offsetX = Random.Range(0, 999f);
        offsetY = Random.Range(0, 999f);
    }

    void Update ()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        
	}
	
	TerrainData GenerateTerrain(TerrainData TData)
    {
        TData.heightmapResolution = width + 1;

        TData.size = new Vector3(width, depth, height);

        TData.SetHeights(0, 0, GenerateHeights());
        return TData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeights(x,y);
            }
        }

        return heights;
    }

    float CalculateHeights(int x, int y)
    {
        float xCord = (float)x / width * scale + offsetX;
        float yCord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCord, yCord);
    }

    float[,] GenerateFalloffMap()
    {
        float[,] map = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float u = x / (float)width;
                float v = y / (float)height;

                float value = Mathf.Max(Mathf.Abs(u), Mathf.Abs(v));
                map[x, y] = value;
            }
        }

        return map;
    }
}
