using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    #region singleton
    public static PlayerCombat instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    PlayerEquipment playerEquipment;
    public Animator animator;
    public bool isAtacking;

    // Start is called before the first frame update
    void Start()
    {
        playerEquipment = PlayerEquipment.instance;
        playerEquipment.onEquipmentChangedCallBack += UpdateStats;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetBool("StartAttack", true);
    }

    void UpdateStats(Equipment newItem, Equipment oldItem)
    {
        Debug.Log("Updating Stats in PlayerCombat Script");
    }
}
