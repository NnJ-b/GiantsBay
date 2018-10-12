using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    public int depth = 20;
    public int width = 256;
    public int height = 256;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public float scale = 20f;

    public terrainType[] Regions;

    private void Start()
    {
        offsetX = Random.Range(0, 999f);
        offsetY = Random.Range(0, 999f);
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData TData)
    {
        TData.heightmapResolution = width + 1;

        TData.size = new Vector3(width, depth, height);

        float[,] heights = GenerateHeights();
        TData.SetHeights(0, 0, heights);
        GenerateCollorMap(TData);
        //TData.SetAlphamaps(0, 0, GenerateCollorMap());
        return TData;
    }

    public float[,] GenerateHeights()
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

    void GenerateCollorMap(TerrainData TData)
    {
        float[,,] map = new float[TData.alphamapWidth, TData.alphamapHeight, 2];      
        
        for (int x = 0; x < TData.alphamapWidth; x++)
        {
            for (int y = 0; y < TData.alphamapHeight; y++)
            {
               if(TData.GetHeight(x,y) >.5)
                {
                    map[x, y, 0] = 1;
                    map[x, y, 1] = 0;
                }else
                {
                    map[x, y, 0] = 0;
                    map[x, y, 1] = 1;
                }
                
            }
        }
        TData.SetAlphamaps(0, 0, map);
        //return map;
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
                Debug.Log(value);
            }
        }

        return map;
    }

    [System.Serializable]
    public struct terrainType
    {
        public string name;
        public float height;
        public Color color;
    }
}
