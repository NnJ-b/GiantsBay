using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public bool fired = false;
    public float lifespan = 5;
    public float speed;


    private void Start()
    {
        transform.position = PlayerEquipment.instance.playerItemAttachmentPoints[1].position;
        transform.rotation = PlayerEquipment.instance.playerItemAttachmentPoints[1].rotation;

        Destroy(gameObject, lifespan);
    }

    private void FixedUpdate()
    {
        if (fired)
        {
            transform.Translate(-Vector3.right * speed, Space.Self);
        }
        else
        {
            transform.position = PlayerEquipment.instance.playerItemAttachmentPoints[1].position;
            transform.rotation = PlayerEquipment.instance.playerItemAttachmentPoints[1].rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit");
        }
    }    
}
