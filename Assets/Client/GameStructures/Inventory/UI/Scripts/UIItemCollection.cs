using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemCollection : MonoBehaviour
{
    [SerializeField]
    private UIInventorySlot _slotPrefab;

    private List<UIInventorySlot> slots;

    public void Initilize(ItemCollection collection)
    {
        slots = new List<UIInventorySlot>();
        foreach (ItemSlot slot in collection.Slots)
        {
            var newSlot = Instantiate(_slotPrefab,transform);
            newSlot.SetSlot(slot);
            slots.Add(newSlot);
        }
    }


}
