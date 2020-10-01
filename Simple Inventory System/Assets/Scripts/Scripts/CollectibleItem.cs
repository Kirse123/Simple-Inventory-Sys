using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField]
    private ItemObject _item;
    public ItemObject Item { get { return _item; } }
    [SerializeField]
    private int _itemAmount;
    public int ItemAmount
    {
        get
        {
            return _itemAmount;
        }
    }

    private BackpackScript targetBackpack;

    private void Start()
    {
        gameObject.tag = "CollectibleItem";
        targetBackpack = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == null)
            return;

        // Check the "Backpack" tag on the gameObject
        if (other.gameObject.tag == "Backpack")
        {
            // Save reference to BackpackScript as potential future target
            targetBackpack = other.gameObject.GetComponent<BackpackScript>();
            // Check for free space on the target backpack
            targetBackpack.CheckPlace(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == null)
            return;

        // Check the "Backpack" tag on the gameObject
        if (other.gameObject.tag == "Backpack")
        {
            // If so, meaning we left backpack boundaries - set target to null
            targetBackpack = null;
            Debug.Log("Exited Backpack");
        }
    }
    private void OnMouseUp()
    {
        // Check for potential target and free space
        if (this.targetBackpack != null && this.targetBackpack.HasPlace)
        {
            // Add current item to the backpack 
            targetBackpack.AddItem(this);
            Destroy(this.gameObject);
        }
    }
}
