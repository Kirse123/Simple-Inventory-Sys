using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Medkit")]
public class MedkitObject : ItemObject
{ 
    private void Awake()
    {
        this._type = ItemType.medkit;
        this._maxAmountInSlot = 3;
    }
}
