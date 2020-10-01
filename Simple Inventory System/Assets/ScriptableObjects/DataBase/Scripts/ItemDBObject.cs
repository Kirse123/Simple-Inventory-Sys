using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data Base Object", menuName = "Inventory System/Data Base")]
public class ItemDBObject : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private ItemObject[] _itemObjects;

    private Dictionary<string, ItemObject> _getObjectByID;

    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        _getObjectByID = new Dictionary<string, ItemObject>();
        for (int i = 0; i < _itemObjects.Length; ++i)
        {
            _getObjectByID.Add(_itemObjects[i].Identifier, _itemObjects[i]);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ItemID"></param>
    /// <returns></returns>
    public ItemObject GetItemByID(string ItemID)
    {
        return _getObjectByID[ItemID];
    }
}
