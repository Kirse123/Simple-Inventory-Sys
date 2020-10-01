﻿using UnityEngine;

public enum ItemType
{
    ammo = 1,
    weapon = 0,
    consumable = 2,
    medkit = 4

}
[System.Serializable]
public abstract class ItemObject : ScriptableObject
{
    [SerializeField]
    protected int _maxAmountInSlot;
    /// <summary>
    /// Maximum item stack size
    /// </summary>
    public int MaxAmountInSlot { get { return _maxAmountInSlot; } }

    [SerializeField]
    protected GameObject _prefab;
    /// <summary>
    /// Prefab reference to represent item on the scene
    /// </summary>
    public GameObject Prefab { get { return _prefab; } }

    [SerializeField]
    protected Sprite _icon;
    /// <summary>
    /// Sprite reference to represent item on UI
    /// </summary>
    public Sprite Icon { get { return _icon; } }

    [SerializeField]
    protected ItemType _type;
    /// <summary>
    /// Type of the item
    /// </summary>
    public ItemType Type { get { return _type; } }

    [SerializeField]
    protected string _identifier = "";
    /// <summary>
    /// Unique item identifier. Generated by UnityEditor
    /// </summary>
    public string Identifier { get { return _identifier; } }

    [SerializeField]
    protected string _itemName;
    /// <summary>
    /// Item name
    /// </summary>
    public string ItemName { get { return _itemName; } }

    [SerializeField]
    protected float _weight;
    /// <summary>
    /// Item weight
    /// </summary>
    public float Weight { get { return _weight; } }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (_identifier == "")
        {
            _identifier = UnityEditor.GUID.Generate().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}