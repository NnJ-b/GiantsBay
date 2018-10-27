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
        SceneManager.LoadScene(1);
    }
}
