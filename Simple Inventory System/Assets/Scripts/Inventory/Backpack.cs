using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Backpack
{
    private BackpackLayout _layout;

    protected List<AbstractInventoryContainer> inventories;
    public List<AbstractInventoryContainer> Inventories
    {
        get
        {
            return inventories;
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

    public abstract void RemoveObject(ItemObject item, int amount);

    public abstract bool CheckPlace(ItemObject item, int amount);
}
