using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInventory : MonoBehaviour {

    private PlayerController controller;

    [Header("Colectables")]
    public int boosters;
    [Tooltip("The Higher the value to longer it will take to reach the asymptote")]
    [Range(10, 1000)]
    public int boosterEffect;
    [Tooltip("raises the asymptote")]
    public float boostMultiplyer = 1;

    private void Start()
    {
        controller = GetComponent<PlayerController>();

        AddBoosters(0);
        CalculateBoosterEffect();
    }

    public float CalculateBoosterEffect()
    {
        if (boosters > 0f)
        {
            float y = boosters + boosterEffect;
            float x = (boosters / y);
            return x;
        }
        else
        {
            return 0f;
        }
    }
    public void AddBoosters(int amount)
    {
        boosters += amount;

        if (boosters < 0)
        {
            boosters = 0;
        }

        if (controller.BoooterCount != null)
        {
            controller.BoooterCount.SetText(boosters.ToString());
        }
        controller.StartCoroutine("ChangeSize");
        SaveLoad.SaveInt("Boosters", boosters);
    }
}
