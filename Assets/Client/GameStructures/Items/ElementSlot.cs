using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStructures.Items
{
    [Serializable]
    public class ElementSlot : IJsonSerializable, IItemSlot
    {
        [SerializeField]
        private Element _element;
        [SerializeField]
        protected int _amount = 0;

        public Item CurrentItem => _element;
        public bool IsEmpty => _element == null;
        public string ItemID => _element == null ? "Empty_Slot" : _element.Id;
        public int Amount { get => IsEmpty ? 0 : _amount; set => _amount = value; }

        public void SetItem(Item item)
        {
            if (item == null)
            {
                Clear();
                return;
            }

            _element = item as Element;

            if (_element == null)
                throw new Exception($"{item}is null or can't convert to {typeof(Element)}");
        }
        public void SetItem(Item item, int amount)
        {
            SetItem(item);

            Amount = amount;
        }
        public void Clear()
        {
            _amount = 0;
            _element = null;
        }

        public Dictionary<string, object> GetObjectData()
        {
            var data = new Dictionary<string, object>();

            if (!IsEmpty)
            {
                data.Add("ID", ItemID);
                data.Add("Amount", Amount);
            }
            return data;
        }

        public void SetObjectData(Dictionary<string, object> data)
        {
            if (data != null)
            {
                var repository = Architecture.Game.GetRepository<ItemsRepository>();
                var id = data["ID"].ToString();
                var amount = System.Convert.ToInt32(data["Amount"]);
                SetItem(repository.GetItem<Element>(id));
                _amount = amount;
            }
        }


    }
}
