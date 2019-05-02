using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float lifespan;
    public float speed;
    public float minHightFromGround;
    public float maxHeightFromGround;
    public Rigidbody rb;

    public void Start()
    {
        transform.position = PlayerEquipment.instance.LeftHand.position;
        transform.rotation = PlayerEquipment.instance.LeftHand.rotation;

        Destroy(gameObject, lifespan);
    }

    public void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 15f)) //check for ground/move up
        {
            float distance = Vector3.Distance(transform.position, hit.point);
            Debug.Log(distance);
            if (distance < minHightFromGround)
            {
                Debug.DrawRay(transform.position, Vector3.down * 15f);
                transform.position = new Vector3(transform.position.x, transform.position.y + (minHightFromGround - distance), transform.position.z);
            }
            else if(distance > maxHeightFromGround)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - (distance - maxHeightFromGround), transform.position.z);
            }
        }
        rb.MovePosition(transform.position + transform.right * speed * -Time.deltaTime); //Move
    }
}
