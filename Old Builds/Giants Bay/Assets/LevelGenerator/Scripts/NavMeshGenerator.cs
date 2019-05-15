using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGenerator : MonoBehaviour {

    public void BuildNavMesh(NavMeshSurface surface)
    {
        surface.BuildNavMesh();
    }
	
}
