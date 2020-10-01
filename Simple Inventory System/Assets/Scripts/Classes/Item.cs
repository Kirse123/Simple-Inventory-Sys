using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    private Slot _attachedTo;
    public  Slot AttachedTo
    {
        get
        {
            return _attachedTo;
        }
        set
        {
            _attachedTo = value;
        }
    }

    private ItemType _type;
    public  ItemType Type { get { return _type; } }

    private long _identifier;
    public  long Identifier { get { return _identifier; } }

    private string _name;
    public  string Name { get { return _name; } }

    private float _weight;
    public  float Weight { get { return _weight; } }

    public Item(long identifier, string name, ItemType type, float weight)
    {
        this._identifier = identifier;
        this._name = name;
        this._type = type;
        this._weight = weight;
        this._attachedTo = null;
    }

    public Item(Item item)
    {
        this._identifier = item.Identifier;
        this._name = item.Name;
        this._type = item.Type;
        this._weight = item.Weight;
        this._attachedTo = null;
    }


    public void Unsnap()
    {
        _attachedTo.Unsnap();
        _attachedTo = null;
    }
}
