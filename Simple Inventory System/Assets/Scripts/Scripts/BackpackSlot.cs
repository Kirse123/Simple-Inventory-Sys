using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackSlot : MonoBehaviour
{
    public ItemType slotType;

    public bool isEmpty
    {
        get
        {
            return _storedItem == null;
        }
    }

    private CollectibleItem _storedItem;
    public CollectibleItem StoredItem
    {
        get
        {
            return _storedItem;
        }
    }

    // Snaps item's gameObject to the slot point 
    public void SnapItem(CollectibleItem item)
    {
        item.gameObject.transform.parent = gameObject.transform;
        // Set isKinematic=false so the item won't fall
        item.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        // Set target for magnite movement and enable movement
        var tmp = item.gameObject.GetComponent<MagnetMovement>();
        tmp.Target = gameObject;
        tmp.enabled = true;

        // Turn Mouse Drag&drop off
        item.gameObject.GetComponent<MouseMoveableObj>().Toggle(false);
        // Turn collectible off
        item.enabled = false;

        // Save stored item ref
        _storedItem = item;
    }
    
    // Unsnap stored item's gameObject 
    public void UnsnapItem()
    {
        _storedItem.enabled = true;

        _storedItem.gameObject.GetComponent<MouseMoveableObj>().Toggle(true);
        _storedItem.gameObject.transform.parent = null;
        _storedItem.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        _storedItem = null;        
    }
    
    //Spawn new object in the slot
    public void ChangeItem(ItemObject item)
    {
        var obj = Instantiate(item.Prefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);

        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.GetComponent<Collider>().enabled = false;
        _storedItem = obj.GetComponent<CollectibleItem>();
        _storedItem.enabled = false;
    }
}
