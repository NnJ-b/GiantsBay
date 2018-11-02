using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor (typeof(SpawnerController))]
public class SpawnerControllerEditor : Editor {

    public override void OnInspectorGUI()
    {
        SpawnerController spawnController = (SpawnerController)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Spawn Random"))
        {
            spawnController.SpawnRandom();
        }
        if (GUILayout.Button("Spawn Chest"))
        {
            spawnController.SpawnChest();
        }
        if (GUILayout.Button("Spawn BuildSite"))
        {
            spawnController.SpawnSite();
        }
    }

}
