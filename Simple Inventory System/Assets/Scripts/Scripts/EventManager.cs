using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemRemovedEvent : UnityEvent<InventorySlot> { }
[System.Serializable]
public class ItemAddedEvent : UnityEvent<InventorySlot> { }

public class EventManager : MonoBehaviour
{
    public static EventManager instance = null;

    public ItemAddedEvent ItemRemoved;
    public ItemAddedEvent ItemAdded;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        { 
            instance = this; 
        }
        else
        { 
            Destroy(gameObject); 
        }

        DontDestroyOnLoad(gameObject);

        ItemAdded.AddListener(OnItemAddedConsole);
        ItemRemoved.AddListener(OnItemRemovededConsole);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnItemAddedConsole(InventorySlot slot)
    {
        Debug.LogFormat("{0} has been added", slot.Item.ItemName);
    }

    private void OnItemRemovededConsole(InventorySlot slot)
    {
        Debug.LogFormat("{0} has been removed", slot.Item.ItemName);
    }
}
