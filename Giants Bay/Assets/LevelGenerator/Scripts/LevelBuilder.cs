using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelBuilder : MonoBehaviour {

    public MapGenerator mapGenerator;

    public NavMeshGenerator navGenerator;
    public NavMeshSurface navMeshSurface;

    public GameObject spawner;

    private void Awake()
    {
        mapGenerator.Generatemap();
        navGenerator.BuildNavMesh(navMeshSurface);
        Instantiate(spawner, Vector3.zero, Quaternion.identity);
    }
}
