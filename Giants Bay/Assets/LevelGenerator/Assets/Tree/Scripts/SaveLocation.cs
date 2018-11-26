using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLocation : MonoBehaviour {

    public static List<GameObject> Trees = new List<GameObject>();

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            Trees.Add(gameObject);

            for (int i = 0; i < Trees.Count; i++)
            {
                SaveLoad.SaveLocation("TreeLocation", Trees[i].transform, i);
            }
            SaveLoad.SaveInt("TreeCount", Trees.Count);
        }
    }
}
