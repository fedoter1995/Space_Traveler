using UnityEngine;
using System.Collections.Generic;
using System;

namespace SpaceTraveler.GameStructures.Items
{
    [System.Serializable]
    public class ItemSlot : IJsonSerializable, IItemSlot
    { 
        [SerializeField]
        private Item _item;
        [SerializeField]
        protected int _amount = 0;


        public Item CurrentItem => _item;
        public string ItemName => _item == null ? "Empty_Slot" : _item.Name;
        public bool IsEmpty => _item == null;
        public bool IsFull => _amount == MaxCapacity;
        public string ItemID => _item == null ? "Empty_Slot" : _item.Id;
        public int Amount { get => IsEmpty ? 0 : _amount; set => _amount = value; }
        public int MaxCapacity => _item == null ? 999 : _item.MaxItemsInSlot;

        public ItemSlot(Item item, int amount)
        {
            _item = item;
            Amount = amount;
        }
        public ItemSlot()
        {
        }

        public void SetItem(Item item)
        {
            if (item == null)
            {
                Clear();
                return;
            }

            _item = item;

        }
        public void SetItem(Item item, int amount)
        {
            SetItem(item);
            Amount = amount;
        }
        public void Clear()
        {
            _amount = 0;
            _item = null;
        }

        public void SetObjectData(Dictionary<string, object> obj)
        {
            if (obj != null)
            {
                var repository = Architecture.Game.GetRepository<ItemsRepository>();
                var id = obj["ID"].ToString();
                var amount = System.Convert.ToInt32(obj["Amount"]);
                SetItem(repository.GetItem<Item>(id));
                _amount = amount;
            }
        }
        public Dictionary<string, object> GetObjectData()
        {
            var dict = new Dictionary<string, object>();

            if (!IsEmpty)
            {
                dict.Add("ID", ItemID);
                dict.Add("Amount", Amount);
            }
            return dict;
        }
    }
    [System.FlagsAttribute]
    public enum SlotType
    {
        Item = 1,
        Equipment = 2
    }
}
