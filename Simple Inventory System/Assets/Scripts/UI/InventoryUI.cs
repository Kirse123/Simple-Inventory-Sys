using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // Inventory container with storing data
    private AbstractInventoryContainer _inventory;

    // All UISlots within
    private UISlot[] _uiSlots;

    // UISlot prefab
    [SerializeField]
    private GameObject slotUIPrefab;

    // UISlot parent for placing
    [SerializeField]
    private GameObject ParentUISlots;

    /// <summary>
    /// Generate slots according to inventory
    /// </summary>
    /// <param name="inventory">Targetr inventory with data</param>
    public void CreateInventoryUI(AbstractInventoryContainer inventory)
    {
        _inventory = inventory;
        _uiSlots = new UISlot[inventory.SlotsAmount];

        // Generate ui slot
        for(int i = 0; i < inventory.SlotsAmount; ++i)
        {
            var obj = Instantiate(slotUIPrefab, ParentUISlots.transform);
            _uiSlots[i] = obj.GetComponent<UISlot>();
            // Set callback on click event
            _uiSlots[i].OnUISlotClickedCallback = OnRemoveSlot;
        }
        UpdateUI();
    }

    /// <summary>
    /// Update all slots within inventory field
    /// </summary>
    public void UpdateUI()
    {
        for(int i = 0; i < _uiSlots.Length; ++i)
        {
            if (i < _inventory.Container.Count)
            {
                _uiSlots[i].AddSlot(_inventory.Container[i]);
                
            }
            else
            {
                _uiSlots[i].ClearSlot();
            }
        }
    }

    /// <summary>
    /// Remove slot from inventory container
    /// </summary>
    /// <param name="slot"></param>
    /// <param name="eventData"></param>
    public void OnRemoveSlot(InventorySlot slot, UnityEngine.EventSystems.PointerEventData eventData)
    {
        _inventory.RemoveItem(slot);
    }
}
