using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public virtual void TakeDamage (float damage)
    {
        Debug.Log("Ouch " + gameObject.name + ":" + damage);
    }
}
