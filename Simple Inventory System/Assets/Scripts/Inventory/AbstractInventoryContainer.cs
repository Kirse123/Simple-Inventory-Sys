using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IStorage
{
    int SlotsTaken { get; }
    int SlotsAmount { get; }
    List<InventorySlot> Container { get; }

    void AddItem(ItemObject item, int amount);
    void RemoveItem(InventorySlot slot, int amount);
    void RemoveItem(InventorySlot slot);
    void RemoveItem(ItemObject item, int amount);
}

// TODO: optimise algorithm for adding
public abstract class AbstractInventoryContainer : IStorage
{
    private int _slotsAmount;
    public int SlotsAmount
    {
        get
        {
            return _slotsAmount;
        }
    }

    public int SlotsTaken
    {
        get
        {
            return _container.Count;
        }
    }

    public int SlotsAvailable
    {
        get
        {
            return _slotsAmount - _container.Count;
        }
    }

    private List<InventorySlot> _container;
    public List<InventorySlot> Container { get { return _container; } }

    public AbstractInventoryContainer(int slotsAmount)
    {
        this._slotsAmount = slotsAmount;
        this._container = new List<InventorySlot>(this._slotsAmount);
    }

    /// <summary>
    /// Add item to inventory
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    public virtual void AddItem(ItemObject item, int amount = 1)
    {
        foreach (InventorySlot slot in _container)
        {
            // Check if there is a slot with same item and enough space
            if (slot.Item.Identifier == item.Identifier && slot.availablePlace >= amount)
            {
                // And in that slot new items
                slot.ChangeAmount(amount);
                // Call UnityEvent
                EventManager.instance.ItemAdded.Invoke(slot);
                return;
            }
        }
        // There is no slot with same item and enough space, so we need new
        _container.Add(new InventorySlot(item, amount));
        // Call UnityEvent
        EventManager.instance.ItemAdded.Invoke(_container[_container.Count - 1]);
    }
    /// <summary>
    /// Check place in the inventory for The item
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public virtual bool CheckPlace(ItemObject item, int amount = 1)
    {
        // Check is there atleast 1 completly empty slot
        if (_container.Count < _container.Capacity)
            return true;
        
        foreach (InventorySlot slot in _container)
        {
            // Check is there the slot with the same item having enough free space
            if (_container.Count < _container.Capacity || (slot.Item.Identifier == item.Identifier) && (slot.availablePlace >= amount))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Remove slot from th einventory
    /// </summary>
    /// <param name="slot"></param>
    public virtual void RemoveItem(InventorySlot slot)
    {
        EventManager.instance.ItemRemoved.Invoke(slot);
        _container.Remove(slot);
    }
    public virtual void RemoveItem(int slotID, int amount)
    {
        _container[slotID].ChangeAmount(amount);
    }

    // Possible extensions for inventory system
    public virtual void RemoveItem(InventorySlot slot, int amount)
    {
        throw new System.NotImplementedException();
    }
    public virtual void RemoveItem(ItemObject item, int amount)
    {
        throw new System.NotImplementedException();
    }
}

/// <summary>
/// Class, representing single inventory slot
/// </summary>
public class InventorySlot
{
    // represents Item in the slot
    private ItemObject _item;
    internal ItemObject Item
    {
        get
        {
            return _item;
        }
        set
        {
            _item = value;
        }
    }

    // represents amount of stored items
    private int _amount;
    internal int Amount
    {
        get
        {
            return _amount;
        }
        set
        {
            _amount = value;
        }
    }

    public bool isFull
    {
        get
        {
            return _item.MaxAmountInSlot == _amount;
        }
    }
    public int availablePlace
    {
        get
        {
            return _item.MaxAmountInSlot - _amount;
        }
    }

    /// <summary>
    /// Create inventory slot with given amount of items, referenced to container
    /// </summary>
    /// <param name="item">Item in the slot</param>
    /// <param name="amount">Amount of Item</param>
    /// <param name="container">reference to the container, where this </param>
    public InventorySlot(ItemObject item, int amount)
    {
        _amount = amount;
        _item = item;
    }

    /// <summary>
    /// Change amount of the stored in the slot item. Can take negative value.
    /// </summary>
    /// <param name="value">Value to increase amount with. Can be negative.</param>
    internal void ChangeAmount(int value)
    {
        _amount += value;
    }
}