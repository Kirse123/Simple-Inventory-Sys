using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBackpack : Backpack
{
    public SimpleBackpack(BackpackLayout layout) : base(layout)
    {
        _inventories = new Dictionary<ItemType, AbstractInventoryContainer>();
        foreach (BackpackLayout.BackpackSlotLayout slotLayout in layout.BackpackSlots)
        {
            _inventories.Add(slotLayout.slotType, new SimpleInventoryContainer(slotLayout.slotCapacity));
        }
    }

    /// <summary>
    /// Add item to backpack
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    public override void AddItem(ItemObject item, int amount = 1)
    {
        _inventories[item.Type].AddItem(item, amount);
    }
    /// <summary>
    /// Check free space in the backpack for item
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public override bool CheckPlace(ItemObject item, int amount = 1)
    {
        return _inventories[item.Type].CheckPlace(item, amount);
    }
    /// <summary>
    /// Remove item from backpack inventory
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    public override void RemoveItem(ItemObject item, int amount)
    {
        _inventories[item.Type].RemoveItem(item, amount);
    }
    /// <summary>
    /// Remove item from backpack inventory
    /// </summary>
    /// <param name="slot"></param>
    /// <param name="amount"></param>
    public override void RemoveItem(InventorySlot slot, int amount)
    {
        _inventories[slot.Item.Type].RemoveItem(slot, amount);
    }
    /// <summary>
    /// Remove item from backpack inventory
    /// </summary>
    /// <param name="slot"></param>
    public override void RemoveItem(InventorySlot slot)
    {
        _inventories[slot.Item.Type].RemoveItem(slot);
    }
}