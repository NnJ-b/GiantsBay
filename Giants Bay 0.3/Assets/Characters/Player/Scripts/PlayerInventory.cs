using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    #region singleton
    public static PlayerInventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple Inventory Scripts!!!");
        }
        instance = this;
    }
    #endregion

    public delegate void OnInventoryChange();
    public OnInventoryChange onInventoryChangeCallBack; //used on inventoryChange
    [Header("Inventory")]
    [SerializeField]
    public List<Item> items = new List<Item>();
    public int strength;
    [Header("Interaction")]
    public float interactableRange;
    public LayerMask interactableMask;

    public void Start()
    {
        StartCoroutine(CheckForItems());
    }

    public IEnumerator CheckForItems()
    {
        while(true)
        {
            Collider[] interactables = Physics.OverlapSphere(transform.position, interactableRange, interactableMask);
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

    public bool AddToInventory(Item item)
    {
        float weight = 0f;
        for (int i = 0; i < items.Count; i++)
        {
            weight += items[i].weight;
        }
        if ((weight += item.weight) > strength)
        {
            Debug.Log("To Heavy");
            return false;
        }
        else
        {
            items.Add(item);
            if (onInventoryChangeCallBack != null)
            {
                onInventoryChangeCallBack.Invoke();
            }
            return true;
        }
    }

    public void RemoveFromInventory(Item item)
    {
        items.Remove(item);
        if(onInventoryChangeCallBack != null)
        {
            onInventoryChangeCallBack.Invoke();
        }
    }
}
