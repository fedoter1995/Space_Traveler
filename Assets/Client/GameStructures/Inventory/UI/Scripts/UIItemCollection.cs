using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemCollection : MonoBehaviour
{
    [SerializeField]
    private UIInventorySlot _slotPrefab;

    private List<UIInventorySlot> slots;

    private ItemCollection collection;

    public void Initilize(ItemCollection collection)
    {
        this.collection = collection;

        slots = new List<UIInventorySlot>();

        foreach (ItemSlot slot in this.collection.Slots)
        {
            var newSlot = Instantiate(_slotPrefab,transform);
            newSlot.SetSlot(slot);
            slots.Add(newSlot);
        }
    }
    public void UpdateSlots()
    {
        if(slots.Count == collection.Slots.Count)
        {
            for(int i = 0; i < slots.Count; i++)
            {
                slots[i].SetSlot(collection.Slots[i]);
            }
        }
        else if(slots.Count < collection.Slots.Count)
        {
            for (int i = 0; i < collection.Slots.Count; i++)
            {
                if(i < slots.Count)
                    slots[i].SetSlot(collection.Slots[i]);
                else
                {
                    var newSlot = Instantiate(_slotPrefab, transform);
                    newSlot.SetSlot(collection.Slots[i]);
                    slots.Add(newSlot);
                }
            }
        }
    }

}
