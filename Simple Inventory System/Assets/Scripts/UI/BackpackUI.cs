using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackUI : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanelPrefab;

    public BackpackScript backpackScript;

    // Refernces to inventoey field for updfate purposes
    private List<InventoryUI> inventoryFields;

    private void Awake()
    {
        inventoryFields = new List<InventoryUI>();
        CreateUI();
    }

    private void CreateUI()
    {
        foreach(var pair in backpackScript.Backpack.Inventories)
        {
            // Instantiate ui inventory fieled
            var obj = Instantiate(inventoryPanelPrefab, gameObject.transform);
            InventoryUI inventoryField = obj.GetComponent<InventoryUI>();
            inventoryField.backpackUI = this;
            // Generate slots for the field
            inventoryField.CreateInventoryUI(pair.Value, pair.Key);
            // Save reference to generated inventory fieled
            inventoryFields.Add(inventoryField);
        }

    }

    public void UpdateUI()
    {
        Debug.Log("UI Updated");
        foreach (InventoryUI inventoryUI in inventoryFields)
        {
            inventoryUI.UpdateUI();
        }
    }
}
