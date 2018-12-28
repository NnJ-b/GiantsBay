using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public bool fired = false;
    public float lifespan = 5;
    public float speed;


    int i = 0;
    private void Start()
    {
        transform.position = PlayerEquipment.instance.playerItemAttachmentPoints[1].position;
        transform.rotation = PlayerEquipment.instance.playerItemAttachmentPoints[1].rotation;
        i = 0;
    }

    private void Update()
    {
        if (fired)
        {
            //StartCoroutine(Die());
            transform.Translate(-Vector3.right * Time.deltaTime * speed, Space.Self);
        }
        else
        {
            transform.position = PlayerEquipment.instance.playerItemAttachmentPoints[1].position;
            transform.rotation = PlayerEquipment.instance.playerItemAttachmentPoints[1].rotation;
        }
    }

    IEnumerator Die()
    {
        while(true)
        {
            if (i == 1)
            {
                Destroy(gameObject);
            }
            i++;
            yield return new WaitForSeconds(lifespan);
        }
    }
}
