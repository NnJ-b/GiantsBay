using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantHouseController : Interactable {

    private PlayerController player;
    public bool occupied = true;
    public bool Owned = true;
    public float timeBetweenCheck;
    public GameObject GiantPrefab;
    public GameObject HousePrefab;

    public static List<GiantHouseController> giantHomes = new List<GiantHouseController>();

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        giantHomes.Add(this);
        for (int i = 0; i < giantHomes.Count; i++)
        {
            SaveLoad.SaveLocation("GiantHouse", giantHomes[i].transform, i);
        }
        SaveLoad.SaveInt("GiantHouseCount", giantHomes.Count);
	}

    public override void Interact()
    {
        base.Interact();
        if(occupied)
        {
            GameObject instance = Instantiate(GiantPrefab, transform.position, Quaternion.identity);
            instance.GetComponent<GiantController>().home = this;
            occupied = false;           
        }
        if(!occupied && !Owned)
        {
            Instantiate(HousePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

 

}
