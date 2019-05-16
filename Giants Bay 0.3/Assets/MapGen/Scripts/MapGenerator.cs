using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{

    public enum DrawMode { NoiseMap, ColorMap, Mesh, FalloffMap };
    public DrawMode drawMode;

    public int mapWidth;
    private int TargetMapWidth;
    public int mapHeight;
    private int TargetMapHeight;
    public float noiseScale;
    public float lerpSpeed;

    public int octives;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool useFalloff;

    public float meshHeightMultiplyer;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public Material material;

    public TerrainType[] regions;

    public float[,] falloffMap;


    private void Start()
    {
        if (mapWidth == 0)
        {
            mapWidth = 100;
            mapHeight = 100;
        }

    }

    public void NewMap()
    {
        seed = Random.Range(-100000, 100000);
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight);
        Generatemap();
    }

    public void ToSmall()
    {
        mapWidth = 100;
        mapHeight = 100;
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight);
        Generatemap();

    }

    public void ToMedium()
    {
        mapWidth = 200;
        mapHeight = 200;
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight);
        Generatemap();

    }
    public void ToLarge()
    {
        mapWidth = 255;
        mapHeight = 255;
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight);
        Generatemap();

    }

    public static class TextureGenerator
    {

        public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height)
        {
            Texture2D texture = new Texture2D(width, height);
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.SetPixels(colorMap);
            texture.Apply();
            return texture;
        }

        public static Texture2D TextureFromHeightMap(float[,] heightMap)
        {
            int width = heightMap.GetLength(0);
            int height = heightMap.GetLength(1);

            //Texture2D texture = new Texture2D(width, height);

            Color[] colorMap = new Color[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
                }
            }
            return TextureFromColorMap(colorMap, width, height);
        }
    }


    public void Generatemap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octives, persistance, lacunarity, offset);
        Color[] colorMap = new Color[mapWidth * mapHeight];

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (useFalloff)
                {
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                }
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplyer, meshHeightCurve), TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));

            if (material != null)
            {
                material.SetFloat("Vector1_9FDFC70B", Noise.minNoiseHeight);
                material.SetFloat("Vector1_A0CF972C", Noise.maxNoiseHeight);
                material.SetFloat("Vector1_CF8F2CE4", meshHeightMultiplyer);
            }



        }
        else if (drawMode == DrawMode.FalloffMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight)));
        }
    }

    private void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octives < 1)
        {
            octives = 0;
        }
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight);
    }

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }
}

public static class MeshGenerator
{

    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplyer, AnimationCurve heightCurve)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;


        MeshData meshData = new MeshData(width, height);
        int vertexIndex = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                meshData.verts[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplyer, topLeftZ - y);
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

                if (x < width - 1 && y < height - 1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }
                vertexIndex++;
            }
        }

        return meshData;

    }
}


public class MeshData
{
    public Vector3[] verts;
    public int[] triangles;
    public Vector2[] uvs;

    int trianglesIndex;

    public MeshData(int meshWidth, int meshHeight)
    {
        verts = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    public void AddTriangle(int a, int b, int c)
    {
        triangles[trianglesIndex] = a;
        triangles[trianglesIndex + 1] = b;
        triangles[trianglesIndex + 2] = c;

        trianglesIndex += 3;
    }

    public Mesh CreatMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        return mesh;
    }
}


public static class FalloffGenerator
{

    public static float[,] GenerateFalloffMap(int width, int height)
    {
        float[,] map = new float[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float i = y / (float)height * 2 - 1;
                float j = x / (float)width * 2 - 1;

                float value = Mathf.Max(Mathf.Abs(i), Mathf.Abs(j));
                map[x, y] = Evaluate(value);
            }
        }
        return map;
    }

    static float Evaluate(float value)
    {
        float a = 3;
        float b = 2.2f;

        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
    }
}

public static class Noise
{

    public static float maxNoiseHeight = float.MinValue;
    public static float minNoiseHeight = float.MaxValue;

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octives, float persistance, float lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octiveOffset = new Vector2[octives];
        for (int i = 0; i < octives; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octiveOffset[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0)
        {
            scale = .0001f;
        }

        //float maxNoiseHeight = float.MinValue;
        //float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octives; i++)
                {
                    float sampleX = (x - halfWidth) / scale * frequency + octiveOffset[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octiveOffset[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;


                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;

                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;

    }

}

public class MinMax
{
    public float min;
    public float max;

    public MinMax()
    {
        min = float.MaxValue;
        max = float.MinValue;
    }

    public void add(float v)
    {
        if(v > max)
        {
            max = v;
        }
        if(v < min)
        {
            min = v;
        }
    }
}

