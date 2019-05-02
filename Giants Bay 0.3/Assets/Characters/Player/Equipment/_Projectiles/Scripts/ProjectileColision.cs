using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileColision : MonoBehaviour
{
    public float damage;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit: " + collision.gameObject.name);
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.health -= damage;
            Destroy(gameObject);
        }
    }
}
