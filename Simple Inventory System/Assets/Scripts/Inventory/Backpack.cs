using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Backpack
{
    private BackpackLayout _layout;

    protected Dictionary<ItemType, AbstractInventoryContainer> _inventories;
    /// <summary>
    /// Contains backpack inventorys
    /// </summary>
    public Dictionary<ItemType, AbstractInventoryContainer> Inventories
    {
        get
        {
            return _inventories;
        }
    }

    public Backpack()
    {

    }

    public Backpack(BackpackLayout layout)
    {
        _layout = layout;
    }

    public abstract void AddItem(ItemObject item, int amount);

    public abstract void RemoveItem(ItemObject item, int amount);
    public abstract void RemoveItem(InventorySlot slot, int amount);
    public abstract void RemoveItem(InventorySlot slot);

    public abstract bool CheckPlace(ItemObject item, int amount);
}
