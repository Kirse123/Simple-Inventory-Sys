using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is used for Backpack object generating
[CreateAssetMenu(fileName = "New Backpack Layout", menuName = "Inventory System/Backpack Layout")]
public class BackpackLayout : ScriptableObject
{

    [System.Serializable]
    public struct BackpackSlotLayout
    {
        public ItemType slotType;
        public int slotCapacity;
    }

    [SerializeField]
    private BackpackSlotLayout[] _backpackSlots;
    /// <summary>
    /// Containes backpack slots
    /// </summary>
    public BackpackSlotLayout[] BackpackSlots
    {
        get
        {
            return _backpackSlots;
        }
    }
}
