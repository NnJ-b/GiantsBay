using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public enum DrawMode{NoiseMap, ColorMap,Mesh,FalloffMap};
    public DrawMode drawMode;

    public int mapWidth;
    private int TargetMapWidth;
    public int mapHeight;
    private int TargetMapHeight;
    public float noiseScale;
    public float lerpSpeed;

    public int octives;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool useFalloff;

    public float meshHeightMultiplyer;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] regions;

    public float[,] falloffMap;

    private void Awake()
    {
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight);
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

    public void Generatemap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octives, persistance, lacunarity, offset);
        Color[] colorMap = new Color[mapWidth * mapHeight];

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if(useFalloff)
                {
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                }
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if(currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if(drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap,mapWidth,mapHeight));
        }
        else if(drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap,meshHeightMultiplyer,meshHeightCurve), TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        }
        else if(drawMode == DrawMode.FalloffMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight)));
        }       
    }

    private void OnValidate()
    {
        if(mapWidth<1)
        {
            mapWidth = 1;
        }
        if(mapHeight<1)
        {
            mapHeight = 1;
        }
        if(lacunarity<1)
        {
            lacunarity = 1;
        }
        if(octives < 1)
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
