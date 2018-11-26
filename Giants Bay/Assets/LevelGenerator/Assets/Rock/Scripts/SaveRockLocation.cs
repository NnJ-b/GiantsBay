using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveRockLocation : MonoBehaviour {

    public static List<GameObject> Rocks = new List<GameObject>();

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            Rocks.Add(gameObject);

            for (int i = 0; i < Rocks.Count; i++)
            {
                SaveLoad.SaveLocation("RockLocation", Rocks[i].transform, i);
            }
            SaveLoad.SaveInt("RockCount", Rocks.Count);
        }
    }
}
