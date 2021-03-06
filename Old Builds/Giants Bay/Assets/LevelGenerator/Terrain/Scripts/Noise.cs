﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {

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
            float offsetY = prng.Next(-100000, 100000)+offset.y;
            octiveOffset[i] = new Vector2(offsetX, offsetY);
        }

        if(scale <= 0)
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
                if(noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if(noiseHeight < minNoiseHeight)
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
