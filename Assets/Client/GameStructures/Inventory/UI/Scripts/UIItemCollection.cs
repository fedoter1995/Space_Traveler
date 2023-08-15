using SpaceTraveler.GameStructures.Items;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.ItemCollections.UI
{
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
            var collectionSlots = this.collection.GetSlots();

            foreach (IItemSlot slot in collectionSlots)
            {
                var newSlot = Instantiate(_slotPrefab,transform);
                newSlot.SetSlot(slot);
                slots.Add(newSlot);
            }
        }
        public void UpdateSlots()
        {
            var collectionSlots = collection.GetSlots();
            if (slots.Count == collectionSlots.Count)
            {
                for(int i = 0; i < slots.Count; i++)
                {
                    slots[i].SetSlot(collectionSlots[i]);
                }
            }
            else if(slots.Count < collectionSlots.Count)
            {
                for (int i = 0; i < collectionSlots.Count; i++)
                {
                    if(i < slots.Count)
                        slots[i].SetSlot(collectionSlots[i]);
                    else
                    {
                        var newSlot = Instantiate(_slotPrefab, transform);
                        newSlot.SetSlot(collectionSlots[i]);
                        slots.Add(newSlot);
                    }
                }
            }
        }

    }
}

