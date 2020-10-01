using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot
{
    private bool _empty;
    public bool IsEmpty { get { return _empty; } }

    private Item _item;
    public Item Item { get { return _item; } }

    private Backpack _backpack;
    public Backpack Backpack { get { return _backpack; } }

    private ItemType _storedItemType;
    public ItemType StoredItemType { get { return _storedItemType; } }

    public Slot(Backpack backpack, ItemType type)
    {
        this._item = null;
        this._empty = false;
        this._backpack = backpack;
        this._storedItemType = type;
    }

    public void Snap(Item newItem)
    {
        if (!this._empty)
        {
            _item.Unsnap();
        }
        _item = newItem;
        newItem.AttachedTo = this;

    }

    public void Unsnap()
    {
        _item = null;
        this._empty = true;
    }
}
