using Newtonsoft.Json.Linq;
using SpaceTraveler.GameStructures.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.ItemCollections
{
    public class Inventory : ItemCollection
    {

        [SerializeField]
        private List<ItemSlot> _itemSlots = new List<ItemSlot>();
        
        public override event Action<object, Item, int> OnAddedEvent;
        public override event Action<Item, int> OnRemovedEvent;
        public override event Action OnInventoryStateChangedEvent;

        public override int GetItemAmount(string itemID)
        {
            var amount = 0;
            var slotsWidthItems = _itemSlots.FindAll(slot => !slot.IsEmpty && slot.ItemID == itemID);
            foreach (IItemSlot slot in slotsWidthItems)
                amount += slot.Amount;

            return amount;
        }
        public override int GetItemAmount(Item item)
        {
            var amount = 0;
            var slotsWidthItems = _itemSlots.FindAll(slot => !slot.IsEmpty && slot.ItemID == item.Id);
            foreach (IItemSlot slot in slotsWidthItems)
                amount += slot.Amount;

            return amount;
        }
        public override List<IItemSlot> GetSlots()
        {
            var slots = new List<IItemSlot>(_itemSlots);
            return slots;
        }
        public override bool HaveItemAmount(Item item, int amount)
        {
            var SlotsWithSameItem = _itemSlots.FindAll(slot => !slot.IsEmpty && slot.ItemID == item.Id);
            var inInventoryAmount = 0;
            
            if(SlotsWithSameItem != null)
                foreach(var slot in SlotsWithSameItem)
                {
                    inInventoryAmount += slot.Amount;

                    if(amount <= inInventoryAmount)
                        return true;
                }

            return false;
        }
        public override bool TryToAddToCollection(object sender, Item item, int amount, out int outputAmount)
        {
            var notEmtySlotWithSameItem = _itemSlots.Find(slot => !slot.IsEmpty && !slot.IsFull && slot.ItemID == item.Id);

            outputAmount = amount;

            if (notEmtySlotWithSameItem == null)
            {
                var emptySlot = _itemSlots.Find(slot => slot.IsEmpty);

                if(emptySlot != null)
                {
                    var remains = TryToAddToSlot(sender, emptySlot, item, amount);
                    
                    outputAmount = remains;
                    
                    if (remains > 0)
                        TryToAddToCollection(sender, item, remains, out outputAmount);

                    return true;
                }

                return false;
            }
            else
            {
                var remains = TryToAddToSlot(sender, notEmtySlotWithSameItem, item, amount);

                outputAmount = remains;

                if (remains > 0)
                    TryToAddToCollection(sender, item, remains, out outputAmount);

                return true;
            }
            
        }
        public override bool TryToAddToCollection(object sender, List<ItemSlot> slots, out List<ItemSlot> outputSlots)
        {
            outputSlots = new List<ItemSlot>();

            foreach (var slot in slots)
            {
                var remains = 0;

                TryToAddToCollection(sender, slot.CurrentItem, slot.Amount, out remains);

                if (remains > 0)
                {
                    outputSlots.Add(new ItemSlot(slot.CurrentItem, remains));
                }
            }

            return outputSlots.Count == 0;

        }
        public override bool TryToRemoveFromCollection(Item item, int amount, out List<ItemSlot> outputSlots)
        {
            outputSlots = new List<ItemSlot>();

            if (HaveItemAmount(item, amount))
            {
                outputSlots = RemoveFromCollection(item, amount);
                return true;
            }

            return false;
        }
        public override bool TryToRemoveFromCollection(Item item, int amount)
        {
            if (HaveItemAmount(item, amount))
            {
                RemoveFromCollection(item, amount);
                return true;
            }
            return false;
        }
        public override void TransitFromSlotToSlot(object sender, ItemSlot fromSlot, ItemSlot toSlot)
        {
           
        }
        public void OnStateChange()
        {
            OnInventoryStateChangedEvent?.Invoke();
        }
        public override Dictionary<string, object> GetObjectData()
        {
            var items = new Dictionary<string, object>();
            var data = new Dictionary<string, object>();

            foreach (ItemSlot slot in _itemSlots)
            {
                items.Add($"{slot.ItemName}", slot.GetObjectData());
            }
            data.Add("Slots", items);

            return data;
        }
        public override void SetObjectData(Dictionary<string, object> data)
        {
            if (data != null)
            {
                JObject jobjSlots = (JObject)data["Slots"];
                var newSlotsData = jobjSlots.ToObject<Dictionary<string, object>>();
                var slots = new List<ItemSlot>();

                foreach (KeyValuePair<string, object> entry in newSlotsData)
                {
                    JObject slotJObject = (JObject)entry.Value;
                    var slotData = slotJObject.ToObject<Dictionary<string, object>>();

                    var slot = new ItemSlot();
                    slot.SetObjectData(slotData);
                    slots.Add(slot);
                }
                _itemSlots = new List<ItemSlot>(slots);
            }
        }
        public override void AddNewEmptySlots(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var newSlot = new ItemSlot();
                _itemSlots.Add(newSlot);
            }
        }
        public override bool TryRemoveEmptySlots(int amount)
        {
            var emptySlots = _itemSlots.FindAll(slot => slot.IsEmpty);

            if (emptySlots.Count >= amount)
            {
                for (int i = 0; i < amount; i++)
                {
                    _itemSlots.Remove(emptySlots[i]);
                }
                return true;
                
            }
            else
                return false;
        }
        private int TryToAddToSlot(object sender, IItemSlot slot, Item item, int amount)
        {
            var maxAmountToAdd = slot.MaxCapacity - slot.Amount;

            if (maxAmountToAdd < amount)
            {
                amount = amount - maxAmountToAdd;

                AddToSlot(sender, slot, item, maxAmountToAdd);

            }
            else
            {
                AddToSlot(sender, slot, item, amount);

                amount = 0;
            }
            return amount;
        }
        private void AddToSlot(object sender, IItemSlot slot, Item item, int amount)
        {
            var clonnedItem = item;

            slot.Amount += amount;

            OnAddedEvent?.Invoke(sender, item, amount);
            OnInventoryStateChangedEvent?.Invoke();
        }
        private List<ItemSlot> RemoveFromCollection(Item item, int amount)
        {
            var outputSlots = new List<ItemSlot>();

            var slotsWithSameItem = _itemSlots.FindAll(slot => !slot.IsEmpty && slot.ItemID == item.Id);

            foreach ( var slot in slotsWithSameItem)
            {
                amount = RemoveFromSlot(slot, amount, ref outputSlots);

                if (amount == 0)
                    break;
            }

            return outputSlots;
        }
        private int RemoveFromSlot(IItemSlot slot, int amount, ref List<ItemSlot> outputSlots)
        {

            if (amount > slot.Amount)
            {
                var newSlot = new ItemSlot(slot.CurrentItem, slot.Amount);
                outputSlots.Add(newSlot);
                amount -= slot.Amount;

                slot.Clear();
            }
            else
            {
                var newSlot = new ItemSlot(slot.CurrentItem, slot.Amount);
                outputSlots.Add(newSlot);
                slot.Amount -= amount;
                
                amount = 0;

                if (slot.Amount == 0)
                    slot.Clear();
            }

            return amount;

        }
    }
}
