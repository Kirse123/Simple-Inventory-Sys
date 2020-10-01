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

    public void SnapItem(CollectibleItem item)
    {
        item.gameObject.transform.parent = gameObject.transform;
        item.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        // Set target for magnite movement
        var tmp = item.gameObject.GetComponent<MagnetMovement>();
        tmp.Target = gameObject;
        tmp.enabled = true;

        item.gameObject.GetComponent<MouseMoveableObj>().Toggle(false);
        item.enabled = false;

        _storedItem = item;
    }

    public void UnsnapItem()
    {
        _storedItem.enabled = true;
        //item.gameObject.transform.position = gameObject.transform.position;
        _storedItem.gameObject.GetComponent<MouseMoveableObj>().Toggle(true);
        _storedItem.gameObject.transform.parent = null;
        _storedItem.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        _storedItem = null;        
    }
    public void ChangeItem(ItemObject item)
    {
        var obj = Instantiate(item.Prefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);

        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.GetComponent<Collider>().enabled = false;
        _storedItem = obj.GetComponent<CollectibleItem>();
        _storedItem.enabled = false;
    }
}
