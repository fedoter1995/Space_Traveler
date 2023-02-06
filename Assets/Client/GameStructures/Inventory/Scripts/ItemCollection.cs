using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class ItemCollection : MonoBehaviour, IJsonSerializable
{
    [SerializeField]
    private bool _saveable = true;
    [SerializeField]
    private List<ItemSlot> _slots = new List<ItemSlot>();

    public List<ItemSlot> Slots
    {
        get
        {
            var newList = new List<ItemSlot>(_slots);
            return newList;
        }
    }

    #region Events
    public event Action<object, Item, int> OnAddedEvent;    
    public event Action<Item, int> OnRemovedEvent;
    public event Action OnItemStateChangedEvent;

    #endregion

    public int GetItemAmount(string itemID)
    {
        var amount = 0;
        var slotsWidthItems = _slots.FindAll(slot => !slot.IsEmpty && slot.ItemID == itemID);
        foreach (ItemSlot slot in slotsWidthItems)
            amount += slot.Amount;

        return amount;
    }
    public ItemSlot GetSlot(string itemID)
    {
        return _slots.Find(slot => slot.ItemID == itemID);
    }
    public List<ItemSlot> GetNotEmptySlots()
    {
        var slots = _slots.FindAll(slot => !slot.IsEmpty);
        return slots;
    }
    public void TryToAddToCollection(object sender, Item item, int amount)
    {
        var SlotWithSameItem = _slots.Find(slot => slot.ItemID == item.Id);

        AddToSlot(sender, SlotWithSameItem, item, amount);

    }
    private void AddToSlot(object sender, ItemSlot slot, Item item, int amount)
    {

        var clonnedItem = item;

        slot.Amount += amount;
        
        OnAddedEvent?.Invoke(sender, item, amount);
        OnStateChange();
    }
    public void OnStateChange()
    {
        OnItemStateChangedEvent?.Invoke();
    }
    public void OnRemove(Item item, int amount)
    {
        OnRemovedEvent?.Invoke(item, amount);
    }

    #region SerializationJSON
    public void SetObjectData(Dictionary<string, object> data)
    {
        if(data != null)
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
            _slots = new List<ItemSlot>(slots);
        }
    }

    public Dictionary<string, object> GetObjectData()
    {
        var items = new Dictionary<string, object>();
        var data = new Dictionary<string, object>();

        foreach (ItemSlot slot in _slots)
        {
            items.Add($"{slot.CurrentItem.Name}", slot.GetObjectData());
        }
        data.Add("Slots", items);

        return data;
    }
    #endregion
}
