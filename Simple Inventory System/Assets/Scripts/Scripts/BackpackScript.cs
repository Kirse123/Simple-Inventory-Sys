using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackpackScript : MonoBehaviour
{
    [SerializeField]
    private Material greenMaterial;
    [SerializeField]
    private Material redMaterial;
    private Material defaultMaterial;

    private Collider _collider;

    private CollectibleItem _targetCollectibleItem;

    [SerializeField]
    private MeshRenderer _meshRenderer;

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

    [SerializeField]
    private GameObject dropPoint;

    [SerializeField]
    private BackpackUI UIWindow;

    [SerializeField]
    private BackpackLayout backpackLayout;

    private Backpack _backpack;
    public Backpack Backpack
    {
        get
        {
            return _backpack;
        }
    }

    private bool _hasPlace;
    public bool HasPlace
    {
        get
        {
            return _hasPlace;
        }
    }

    private bool isDraging = false;

    private Dictionary<ItemType, BackpackSlot> getBackpackSlots;

    private void Start()
    {
        _backpack = new SimpleBackpack(backpackLayout);
        _collider = GetComponent<Collider>();

        getBackpackSlots = new Dictionary<ItemType, BackpackSlot>();
        getBackpackSlots.Add(ItemType.Weapon, _weaponSlot);
        getBackpackSlots.Add(ItemType.Armor, _armorSlot);
        getBackpackSlots.Add(ItemType.Consumable, _consumablesSlot);

        gameObject.tag = "Backpack";
        defaultMaterial = _meshRenderer.material;
        UIWindow.backpackScript = this;

        EventManager.instance.ItemRemoved.AddListener(DropItem);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CollectibleItem")
        {
            _meshRenderer.material = defaultMaterial;
        }
    }

    public void CheckPlace(CollectibleItem collectibleItem)
    {
        _hasPlace = _backpack.CheckPlace(collectibleItem.Item, collectibleItem.ItemAmount);
        if (_hasPlace)
            _meshRenderer.material = greenMaterial;
        else
            _meshRenderer.material = redMaterial;
    }
    public void AddItem(CollectibleItem collectibleItem)
    {
        _backpack.AddItem(collectibleItem.Item, collectibleItem.ItemAmount);
        _meshRenderer.material = defaultMaterial;

        var backpackSlot = GetBackpackSlot(collectibleItem.Item.Type);

        if (backpackSlot.isEmpty)
        {
            backpackSlot.SnapItem(collectibleItem);
            return;
        }

        // if we do not snap object, then put it in and destroy 
        Destroy(collectibleItem.gameObject);
    }
    public void DropItem(InventorySlot slot)
    {
        var backpackSlot = GetBackpackSlot(slot.Item.Type);

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

        // If there is different item in slot. then just drop this on the floor
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
