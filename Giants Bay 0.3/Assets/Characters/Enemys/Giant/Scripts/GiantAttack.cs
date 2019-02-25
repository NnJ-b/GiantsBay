using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantAttack
{
    public bool PlayerInRange(Transform player, Transform enemy, float range)
    {
        if(Vector3.Distance(player.position, enemy.position) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void HitPlayer(Transform player, float damageAmount)
    {
        player.GetComponent<PlayerStats>().TakeDamage(damageAmount);
    }

}
