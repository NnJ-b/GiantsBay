﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerTouch))]
[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerAnimation))]
public class PlayerController : MonoBehaviour {

    public Camera cam;
    public PlayerMotor motor;
    public PlayerTouch touch;
    public PlayerInventory inventory;
    public PlayerCombat combat;
    public PlayerAnimation animationController;

    [Header("Followers")]
    public List<FollowerController> followers = new List<FollowerController>();
    public int capacity;


    [Header("UI References")]
    public Slider healthBar;
    public Slider sizeBar;
    public TextMeshProUGUI BoooterCount;
    public TextMeshProUGUI follwerPerCapacity;
    private void Awake()
    {      
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        touch = GetComponent<PlayerTouch>();
        inventory = GetComponent<PlayerInventory>();
        combat = GetComponent<PlayerCombat>();
        animationController = GetComponent<PlayerAnimation>();
        capacity = HouseController.Capacity;

        LoadPlayerValues();
    }

    private void LoadPlayerValues()
    {
        inventory.boosters = SaveLoad.LoadInt("Boosters");
        combat.health = SaveLoad.LoadInt("Health");
        if(combat.health == 0)
        {
            combat.health = 100;
        }
    }   
    
    public IEnumerator ChangeSize()
    {
        if(sizeBar != null)
        {
            sizeBar.value = inventory.CalculateBoosterEffect();
        }

        float x = 1 + (inventory.CalculateBoosterEffect() * inventory.boostMultiplyer);

        while (transform.localScale.x < x)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(x, x, x), animationController.sizeLerpSpeed);
            yield return new WaitForEndOfFrame();
        }  
    }
}
