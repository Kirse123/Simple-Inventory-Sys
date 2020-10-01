using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackUI : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanelPrefab;

    [SerializeField]
    private BackpackScript backpackScript;

    private List<InventoryUI> inventoryFlields;

    private bool _created;

    private void Start()
    {
        inventoryFlields = new List<InventoryUI>();
        CreateUI();
    }

    private void OnEnable()
    {
        if(_created)
            UpdateUI();
    }

    private void CreateUI()
    {
        foreach (AbstractInventoryContainer container in backpackScript.Backpack.Inventories)
        {
            // Instamtiate ui inventory filed
            var obj = Instantiate(inventoryPanelPrefab, gameObject.transform);
            InventoryUI invPanel = obj.GetComponent<InventoryUI>();
            inventoryFlields.Add(invPanel);
            // Generate slots for the field
            invPanel.CreateInventoryUI(container);
        }
        _created = true;
    }

    public void UpdateUI()
    {
        foreach (InventoryUI inventoryField in inventoryFlields)
        {
            inventoryField.UpdateUI();
        }
    }
}
