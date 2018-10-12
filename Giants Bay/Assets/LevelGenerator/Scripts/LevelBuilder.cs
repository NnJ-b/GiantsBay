using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelBuilder : MonoBehaviour {

    public MapGenerator mapGenerator;

    public NavMeshGenerator navGenerator;
    public NavMeshSurface navMeshSurface;

    private void Awake()
    {
        mapGenerator.Generatemap();
        navGenerator.BuildNavMesh(navMeshSurface);
    }
}
