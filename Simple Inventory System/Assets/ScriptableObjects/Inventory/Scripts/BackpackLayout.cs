using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Backpack Layout", menuName = "Inventory System/Backpack Layout")]
public class BackpackLayout : ScriptableObject
{
    [SerializeField]
    private int _weaponSlotsAmount;
    /// <summary>
    /// Maximum amount of inventory slots for weapon
    /// </summary>
    public int WeaponSlotsAmount
    {
        get
        {
            return _weaponSlotsAmount;
        }
    }

    [SerializeField]
    private int _consumablesSlotsAmount;
    /// <summary>
    /// Maximum amount of inventory slots for consumables
    /// </summary>
    public int ConsumablesSlotsAmount
    {
        get
        {
            return _consumablesSlotsAmount;
        }
    }

    [SerializeField]
    private int _ammoSlotsAmount;
    /// <summary>
    /// Maximum amount of inventory slots for ammo
    /// </summary>
    public int AmmoSlotsAmount
    {
        get
        {
            return _ammoSlotsAmount;
        }
    }
}
