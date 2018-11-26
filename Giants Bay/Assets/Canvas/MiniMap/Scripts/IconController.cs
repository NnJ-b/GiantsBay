using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconController : MonoBehaviour {

    public Buildings referenceBuilding;
    public CanvasController canvasController;

    private void Start()
    {
        canvasController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>();
    }

    public void FarmIcon()
    {
        if(canvasController.followerControll == true)
        {
            canvasController.followerBeingControled.assignedFarm = EventSystem.current.currentSelectedGameObject.GetComponent<IconController>().referenceBuilding.transform;
            canvasController.followerBeingControled.MoveToAssignedFarm();
        }
        else
        {
            int occupantCount = EventSystem.current.currentSelectedGameObject.GetComponent<IconController>().referenceBuilding.Ocupants.Count;
            int maxCappacity = HouseController.CapacityPerHome;
            canvasController.UpdateMapInfo(occupantCount, maxCappacity);
        }
    }

    public void HouseIcon()
    {

    }
	
}
