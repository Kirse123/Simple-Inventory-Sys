using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//TODO: Remove action in UI rethink
public class UISlot : MonoBehaviour
{
    public delegate void OnUISlotClicked(InventorySlot slot);
    public OnUISlotClicked OnUISlotClickedCallback;

    // Inventory slot, representet by this UISlot
    private InventorySlot _inventorySlot = null;

    // Image for item icon
    [SerializeField]
    private Image _itemImage = null;

    /// <summary>
    /// Add reference to inventory slot and set icon
    /// </summary>
    /// <param name="inventorySlot"></param>
    public void AddSlot(InventorySlot inventorySlot)
    {
        _inventorySlot = inventorySlot;
        _itemImage.sprite = _inventorySlot.Item.Icon;
        _itemImage.enabled = true;
    }

    /// <summary>
    /// Clear UI Inventory slot. Also Deletes icon
    /// </summary>
    public void ClearSlot()
    {
        _inventorySlot = null;
        _itemImage.sprite = null;
        _itemImage.enabled = false;
    }

    public void RemoveItem()
    {
        UnhighlightIcon();
        OnUISlotClickedCallback(this._inventorySlot);
    }

    public void HighlightIcon()
    {
        _itemImage.color = Color.grey;
    }

    public void UnhighlightIcon()
    {
        _itemImage.color = Color.white;
    }

    /*
    /// <summary>
    /// Remove item from inventory
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        Debug.LogFormat("Item {0} Droped", _inventorySlot.Item.ItemName);
        if (this._inventorySlot != null)
            OnUISlotClickedCallback(this._inventorySlot);
    }
    */
}
