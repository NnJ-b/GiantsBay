using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSiteController : Interactable {

    public GameObject farmPreFab;
    public GameObject HousePreFab;

    private GameObject canvas;
    private GameObject canvasChild;
    private bool showingOptions = false;

    public void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        canvasChild = canvas.transform.Find("BuildingOptionPopUp").gameObject;
    }

    public override void Interact()
    {
        base.Interact();
        canvasChild.SetActive(true);
        canvas.GetComponent<CanvasController>().buildSiteController = this;
    }

    public override void StopFocus()
    {
        base.StopFocus();
        if (canvasChild != null)
        {
            canvasChild.SetActive(false);
        }
    }

    public void SpawnHouse()
    {
        SpawnObject(HousePreFab);
    }
    public void SpawnFarm()
    {
        SpawnObject(farmPreFab);

    }

    private void SpawnObject(GameObject objectToSpawn)
    {
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        canvasChild.SetActive(false);
        Destroy(gameObject);
    }
}
