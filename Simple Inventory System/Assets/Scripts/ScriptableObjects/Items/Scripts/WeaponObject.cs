using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapon")]
public class WeaponObject : ItemObject
{
    private void Awake()
    {
        this._type = ItemType.Weapon;
        this._maxAmountInSlot = 1;
    }
}
