using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveFoliageLocation {

    public static void Save(List<GameObject> listToSave, string name)
    {
        for (int i = 0; i < listToSave.Count; i++)
        {
            SaveLoad.SaveLocation(name, listToSave[i].transform, i);
        }
    }
}
