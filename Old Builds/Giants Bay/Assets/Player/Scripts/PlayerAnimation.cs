using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimation : MonoBehaviour {

    private PlayerController controller;

    [Header("Animation Controlls")]
    public Animator animator;
    [Range(.001f, 1)]
    public float sizeLerpSpeed;
    public bool attacking = false;
    public bool endAttack = false;
    public bool startAttack = false;
    public bool attackReady = false;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }
}
