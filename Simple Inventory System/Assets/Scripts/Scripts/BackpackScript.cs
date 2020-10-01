using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackpackScript : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private Material greenMaterial;
    [SerializeField]
    private Material redMaterial;
    private Material defaultMaterial;

    // Reference to trigger collider
    private Collider _collider;

    // Potential item to collect
    private CollectibleItem _targetCollectibleItem;

    // Slots for snaping item
    [SerializeField]
    private BackpackSlot _weaponSlot;
    public BackpackSlot WeaponSlot
    {
        get
        {
            return _weaponSlot;
        }
    }
    [SerializeField]
    private BackpackSlot _armorSlot;
    public BackpackSlot ArmorSlot
    {
        get
        {
            return _armorSlot;
        }
    }
    [SerializeField]
    private BackpackSlot _consumablesSlot;
    public BackpackSlot ConsumablesSlot
    {
        get
        {
            return _consumablesSlot;
        }
    }

    // Point in space for dropping removed items
    [SerializeField]
    private GameObject dropPoint;

    // Reference to Backpack UI window
    [SerializeField]
    private BackpackUI UIWindow;

    // Backpack storage layout. Used when creating new backpack
    [SerializeField]
    private BackpackLayout backpackLayout;

    // Main item storage
    private Backpack _backpack;
    public Backpack Backpack
    {
        get
        {
            return _backpack;
        }
    }

    //Variable to flag drag&drop events
    private bool isDraging = false;
    private bool _hasPlace;
    public bool HasPlace
    {
        get
        {
            return _hasPlace;
        }
    }

    private void Start()
    {
        _backpack = new SimpleBackpack(backpackLayout);
        _collider = GetComponent<Collider>();

        gameObject.tag = "Backpack";
        defaultMaterial = _meshRenderer.material;
        UIWindow.backpackScript = this;

        EventManager.instance.ItemRemoved.AddListener(DropItem);
    }

    // Check if there is free space for the item and set appropriate material as highlight
    public void CheckPlace(CollectibleItem collectibleItem)
    {
        _hasPlace = _backpack.CheckPlace(collectibleItem.Item, collectibleItem.ItemAmount);
        if (_hasPlace)
            _meshRenderer.material = greenMaterial;
        else
            _meshRenderer.material = redMaterial;
    }

    // Restore default material when not colliding with collectible items
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CollectibleItem")
        {
            _meshRenderer.material = defaultMaterial;
        }
    }

    // Add collcetible to inventory
    public void AddItem(CollectibleItem collectibleItem)
    {
        // Add item to inventory
        _backpack.AddItem(collectibleItem.Item, collectibleItem.ItemAmount);
        _meshRenderer.material = defaultMaterial;

        // Get potential item-slnapping-slot
        var backpackSlot = GetBackpackSlot(collectibleItem.Item.Type);

        if (backpackSlot.isEmpty)
        {
            backpackSlot.SnapItem(collectibleItem);
            return;
        }

        // if we do not snap object, then put it in inventory and destroy gameObject
        Destroy(collectibleItem.gameObject);
    }
    
    // Drops Item's gameObject. Is called on Event "ItemRemoved"
    public void DropItem(InventorySlot slot)
    {
        // Get potential item-slnapping-slot
        var backpackSlot = GetBackpackSlot(slot.Item.Type);

        // If dropping item is snapped to the slot
        if (slot.Item == backpackSlot.StoredItem.Item)
        {
            backpackSlot.UnsnapItem();
            // Check if the item we are dropping is the last of its type in the inventory
            if (_backpack.Inventories[slot.Item.Type].SlotsTaken > 1)
            {
                // Change item in backpack slot to the next in the inventory
                backpackSlot.ChangeItem((_backpack.Inventories[slot.Item.Type].Container[1].Item));
            }
            return;
        }

        // If there is different item in the slot, then just drop this one on the floor
        Instantiate(slot.Item.Prefab, dropPoint.transform.position, Quaternion.identity, null);
    }

    public void SetDraging()
    {
        isDraging = true;
    }
    public void OpenUI()
    {
        UIWindow.gameObject.SetActive(true);
        UIWindow.UpdateUI();
        isDraging = false;
    }
    public void CloseUI()
    {
        if (isDraging)
        {
            return;
        }
        UIWindow.gameObject.SetActive(false);
    }

    // Returns appropriate item-snapping-slot for given item type
    private BackpackSlot GetBackpackSlot(ItemType type)
    {
        switch (type)
        {
            case ItemType.Weapon:
                return _weaponSlot;
            case ItemType.Consumable:
                return _consumablesSlot;
            case ItemType.Armor:
                return _armorSlot;
            default:
                return null;
        }
    }
}
