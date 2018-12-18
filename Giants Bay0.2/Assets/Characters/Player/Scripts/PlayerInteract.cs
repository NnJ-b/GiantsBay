using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interactable")]
    public float interactableRange = 5f;
    public LayerMask mask;

    private void Start()
    {
        StartCoroutine("CheckForInteractables");
    }

    private IEnumerator CheckForInteractables()
    {
        while (true)
        {
            Collider[] interactables = Physics.OverlapSphere(transform.position, interactableRange, mask);
            float minDistance = float.MaxValue;
            Collider closestObject = null;
            for (int i = 0; i < interactables.Length; i++)
            {
                float distance = DistanceToObject(interactables[i].transform);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestObject = interactables[i];
                }
            }
            if (closestObject != null)
            {
                //Give choice of interacting
                closestObject.gameObject.GetComponent<Interactable>().Interact();
            }
            yield return new WaitForSeconds(.4f);
        }
    }

    private float DistanceToObject(Transform location)
    {
        return Vector3.Distance(transform.position, location.position);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, interactableRange);
    }
}
