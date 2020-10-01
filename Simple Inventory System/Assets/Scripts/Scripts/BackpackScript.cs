using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackScript : MonoBehaviour
{
    [SerializeField]
    private Material greenMaterial;
    [SerializeField]
    private Material redMaterial;
    private Material defaultMaterial;

    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private BackpackLayout backpackLayout;

    private Backpack _backpack;
    public Backpack Backpack
    {
        get { return _backpack; }
    }
    private Collider _collider;
    private CollectibleItem _targetCollectibleItem;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CollectibleItem")
        {
            //_meshRenderer.material = greenMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CollectibleItem")
        {
            _meshRenderer.material = defaultMaterial;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Show Inventory UI");
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
    }
}
