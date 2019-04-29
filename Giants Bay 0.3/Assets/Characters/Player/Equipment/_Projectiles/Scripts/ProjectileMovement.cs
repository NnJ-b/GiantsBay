using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float lifespan;
    public float speed;

    public void Start()
    {
        transform.position = PlayerEquipment.instance.LeftHand.position;
        transform.rotation = PlayerEquipment.instance.LeftHand.rotation;

        Destroy(gameObject, lifespan);
    }

    public void FixedUpdate()
    {
        transform.Translate(-Vector3.right * speed, Space.Self);
    }
}
