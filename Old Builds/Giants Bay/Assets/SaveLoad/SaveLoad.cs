using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoad{

    public static void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public static void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }
    public static void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public static string LoadString(string key)
    {
        if(PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }
        else
        {
            return null;
        }
    }

    public static float LoadFloat(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetFloat(key);
        }
        else
        {
            return 0f;
        }
    }

    public static int LoadInt(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }
        else
        {
            return 0;
        }
    }


    public static void SaveLocation(string type, Transform location, int count)
    {
        SaveFloat(type + count + "x", location.position.x);
        SaveFloat(type + count + "y", location.position.y);
        SaveFloat(type + count + "z", location.position.z);
    }

    public static Vector3 LoadLocation(string type, int count)
    {
        return new Vector3(LoadFloat(type + count + "x"), LoadFloat(type + count + "y"), LoadFloat(type + count + "z"));
    }


}
