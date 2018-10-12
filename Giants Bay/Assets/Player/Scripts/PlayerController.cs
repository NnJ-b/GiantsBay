using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
    private Camera camera;

    public NavMeshAgent navMeshAgent;
    public float rayDistance = 500;
    public float spawnOffset=.5f;

    public LayerMask ground;

	void Start () {
        camera = Camera.main;
	}
    private void Awake()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, Mathf.Infinity, ground))
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
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance, ground))
            {
                if(hit.collider.tag == "Ground")
                {
                    navMeshAgent.SetDestination(hit.point);
                }
            }
        }
	}
}
