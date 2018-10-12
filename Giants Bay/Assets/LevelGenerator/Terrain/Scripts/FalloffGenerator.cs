using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FalloffGenerator { 

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
                map[x,y] = Evaluate(value);
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
