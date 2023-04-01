using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using GameStructures.Items;
using Newtonsoft.Json.Linq;

[Serializable]
public class Inventory : ItemCollection
{
    [SerializeField]
    private List<ElementSlot> _elements = new List<ElementSlot>();

    public override event Action<object, Item, int> OnAddedEvent;
    public override event Action<Item, int> OnRemovedEvent;
    public override event Action OnItemStateChangedEvent;

    public override List<IItemSlot> GetSlots()
    {
        return new List<IItemSlot>(_elements);
    }
    public override int GetItemAmount(string itemID)
    {
        var amount = 0;
        var slotsWidthItems = _elements.FindAll(slot => !slot.IsEmpty && slot.ItemID == itemID);
        foreach (IItemSlot slot in slotsWidthItems)
            amount += slot.Amount;

        return amount;
    }
    public override IItemSlot GetSlot(string itemID)
    {
        return _elements.Find(slot => slot.ItemID == itemID);
    }
    public override List<IItemSlot> GetNotEmptySlots()
    {
        var slots = new List<IItemSlot>(_elements.FindAll(slot => !slot.IsEmpty));

        return slots;
    }
    public override void TryToAddToCollection(object sender, Item item, int amount)
    {
        var SlotWithSameItem = _elements.Find(slot => slot.ItemID == item.Id);

        if (SlotWithSameItem != null)
            AddToSlot(sender, SlotWithSameItem, item, amount);
        else
        {
            SlotWithSameItem = new ElementSlot();
            SlotWithSameItem.SetItem(item, amount);
            _elements.Add(SlotWithSameItem);
        }
    }
    public override void TryToAddToCollection(object sender, List<ItemSlot> slots)
    {
        foreach(ItemSlot itemSlot in slots)
        {
            var SlotWithSameItem = _elements.Find(slot => slot.ItemID == itemSlot.ItemID);

            if(SlotWithSameItem != null)
                AddToSlot(sender, SlotWithSameItem, itemSlot.CurrentItem, itemSlot.Amount);
            else
            {
                SlotWithSameItem = new ElementSlot();
                SlotWithSameItem.SetItem(itemSlot.CurrentItem, itemSlot.Amount);
                _elements.Add(SlotWithSameItem);
            }
        }
    }
    public override void OnStateChange()
    {
        OnItemStateChangedEvent?.Invoke();
    }
    public override bool TryToRemove(Item item, int amount)
    {
        var SlotWithSameItem = _elements.Find(slot => slot.ItemID == item.Id);

        if (SlotWithSameItem.Amount >= amount)
        {
            SlotWithSameItem.Amount -= amount;
            OnRemovedEvent?.Invoke(item, amount);
            return true;
        }

        return false;
    }
    public override void SetObjectData(Dictionary<string, object> data)
    {
        if (data != null)
        {
            JObject jobjSlots = (JObject)data["Slots"];
            var newSlotsData = jobjSlots.ToObject<Dictionary<string, object>>();
            var slots = new List<ElementSlot>();

            foreach (KeyValuePair<string, object> entry in newSlotsData)
            {
                JObject slotJObject = (JObject)entry.Value;
                var slotData = slotJObject.ToObject<Dictionary<string, object>>();

                var slot = new ElementSlot();
                slot.SetObjectData(slotData);
                slots.Add(slot);
            }
            _elements = new List<ElementSlot>(slots);
        }
    }
    public override Dictionary<string, object> GetObjectData()
    {
        var items = new Dictionary<string, object>();
        var data = new Dictionary<string, object>();

        foreach (ElementSlot slot in this._elements)
        {
            items.Add($"{slot.CurrentItem.Name}", slot.GetObjectData());
        }
        data.Add("Slots", items);

        return data;
    }
    public override bool HaveItemAmount(Item item, int amount)
    {
        var SlotWithSameItem = _elements.Find(slot => slot.ItemID == item.Id);

        if (SlotWithSameItem.Amount >= amount)
            return true;

        return false;
    }
    public override string ToString()
    {
        return "Spaceship Inventory";
    }
    private void AddToSlot(object sender, IItemSlot slot, Item item, int amount)
    {
        var clonnedItem = item;

        slot.Amount += amount;

        OnAddedEvent?.Invoke(sender, item, amount);
        OnStateChange();
    }
}
