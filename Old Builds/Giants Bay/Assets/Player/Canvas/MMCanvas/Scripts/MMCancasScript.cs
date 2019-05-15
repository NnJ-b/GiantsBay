using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMCancasScript : MonoBehaviour {

    public MapGenerator mapGenerator;

    public GameObject mainMenu;

    // Use this for initialization
    void Start()
    {
        HideAll();
        mainMenu.SetActive(true);
    }

    private void HideAll()
    {
        mainMenu.SetActive(false);
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SaveLoad.SaveInt("Seed", mapGenerator.seed);
        SaveLoad.SaveInt("MapWidth", mapGenerator.mapWidth);
        SaveLoad.SaveInt("MapHeight", mapGenerator.mapHeight);
        SaveLoad.SaveInt("NewGame", 1);
        Buildings.Farms.Clear();
        Buildings.Homes.Clear();
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
    }
}
