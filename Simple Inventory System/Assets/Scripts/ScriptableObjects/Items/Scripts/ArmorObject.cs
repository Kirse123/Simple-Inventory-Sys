using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Armor")]
public class ArmorObject : ItemObject
{
    private void Awake()
    {
        this._type = ItemType.Armor;
        this._maxAmountInSlot = 1;
    }
}
