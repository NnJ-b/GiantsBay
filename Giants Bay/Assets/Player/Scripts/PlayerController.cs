using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    private Camera cam;

    [Header("Ray Trace ")]
    public NavMeshAgent navMeshAgent;
    public float rayDistance = 500;
    public LayerMask walkable;

    [Header("Spawn")]
    public float spawnOffset = 1f;


    void Start () {
        cam = Camera.main;
	}
    private void Awake()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, Mathf.Infinity, walkable))
        {
            if(hit.collider.tag == "Ground")
            {
                transform.position = new Vector3(transform.position.x, hit.point.y+spawnOffset, transform.position.z);
            }
        }
    }

    void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance, walkable))
            {
                if(hit.collider.tag == "Ground")
                {
                    navMeshAgent.SetDestination(hit.point);
                }
            }
        }
	}
}
