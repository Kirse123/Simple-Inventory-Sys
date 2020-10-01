using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBackpack : Backpack
{
    //TODO: set inventory ID in inventories to ItemType value. Array??
    public SimpleBackpack(BackpackLayout layout) : base(layout)
    {
        inventories = new List<AbstractInventoryContainer>();
        // Set weapon slots
        inventories.Add(new SimpleInventoryContainer(layout.WeaponSlotsAmount));
        // Set ammo slots
        inventories.Add(new SimpleInventoryContainer(layout.AmmoSlotsAmount));
        // Set consumables slots
        inventories.Add(new SimpleInventoryContainer(layout.ConsumablesSlotsAmount));
    }
    public override void AddItem(ItemObject item, int amount = 1)
    {        
        switch (item.Type)
        {
            case ItemType.weapon:
                inventories[0].AddItem(item, amount);
                break;
            case ItemType.ammo:
                inventories[1].AddItem(item, amount);
                break;
            case ItemType.consumable:
                inventories[2].AddItem(item, amount);
                break;
        }

        Debug.LogFormat("{0}({1}) has been added. ", item.ItemName, amount);
    }

    public override bool CheckPlace(ItemObject item, int amount = 1)
    {
        switch (item.Type)
        {
            // Check place for each type stored in this backpack
            case ItemType.weapon:
                return inventories[0].CheckPlace(item, amount);              
            case ItemType.ammo:
                return inventories[1].CheckPlace(item, amount);
            case ItemType.consumable:
                return inventories[2].CheckPlace(item, amount);
            // If not storing such type just return false
            default:
                return false;
        }
    }

    public override void RemoveObject(ItemObject item, int amount)
    {
        switch (item.Type)
        {
            // Check place for each type stored in this backpack
            case ItemType.weapon:
                inventories[0].RemoveItem(item, amount);
                break;
            case ItemType.ammo:
                inventories[0].RemoveItem(item, amount);
                break;
            case ItemType.consumable:
                inventories[0].RemoveItem(item, amount);
                break;
        }
    }
}
