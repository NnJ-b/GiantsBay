﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : HumanClass {

    private PlayerController player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
