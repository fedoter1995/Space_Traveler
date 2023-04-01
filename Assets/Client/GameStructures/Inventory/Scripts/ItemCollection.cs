using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using Newtonsoft.Json.Linq;
using GameStructures.Items;

[Serializable]
public abstract class ItemCollection : IJsonSerializable
{

    #region Events
    public abstract event Action<object, Item, int> OnAddedEvent;    
    public abstract event Action<Item, int> OnRemovedEvent;
    public abstract event Action OnItemStateChangedEvent;

    #endregion
    public abstract List<IItemSlot> GetSlots();
    public abstract int GetItemAmount(string itemID);
    public abstract IItemSlot GetSlot(string itemID);
    public abstract List<IItemSlot> GetNotEmptySlots();
    public abstract void TryToAddToCollection(object sender, Item item, int amount);
    public abstract void TryToAddToCollection(object sender, List<ItemSlot> slots);
    public abstract void OnStateChange();
    public abstract bool TryToRemove(Item item, int amount);

    #region SerializationJSON
    public abstract void SetObjectData(Dictionary<string, object> data);
    public abstract Dictionary<string, object> GetObjectData();
    public abstract bool HaveItemAmount(Item item, int amount);

    #endregion
}
