using System;
using System.Collections.Generic;
using SpaceTraveler.GameStructures.Items;

namespace SpaceTraveler.GameStructures.ItemCollections
{
    [Serializable]
    public abstract class ItemCollection : IJsonSerializable
    {

        #region Events
        public abstract event Action<object, Item, int> OnAddedEvent;    
        public abstract event Action<Item, int> OnRemovedEvent;
        public abstract event Action OnInventoryStateChangedEvent;

        #endregion
        public abstract List<IItemSlot> GetSlots();
        public abstract int GetItemAmount(string itemID);
        public abstract int GetItemAmount(Item item);
        public abstract void AddNewEmptySlots(int amount);
        public abstract bool TryRemoveEmptySlots(int amount);
        public abstract bool TryToAddToCollection(object sender, Item item, int amount, out int outputAmount);
        public abstract bool TryToAddToCollection(object sender, List<ItemSlot> slots, out List<ItemSlot> outputSlots);
        public abstract bool TryToRemoveFromCollection(Item item, int amount, out List<ItemSlot> outputSlots);
        public abstract bool TryToRemoveFromCollection(Item item, int amount);
        public abstract void TransitFromSlotToSlot(object sender, ItemSlot fromSlot, ItemSlot toSlot);
        public abstract bool HaveItemAmount(Item item, int amount);
        #region SerializationJSON
        public abstract void SetObjectData(Dictionary<string, object> data);
        public abstract Dictionary<string, object> GetObjectData();

        #endregion
    }
}

