using SpaceTraveler.GameStructures.ItemCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.UI.ItemStorage
{
    public class ItemCollectionPanel : MonoBehaviour
    {
        [SerializeField]
        protected List<UIItemSlot> _uiSlots = new List<UIItemSlot>();
        public ItemCollection ItemCollection { get; set; }


        protected virtual void InitUISlots()
        {
            _uiSlots = GetComponentsInChildren<UIItemSlot>().ToList();
            foreach (UIItemSlot slot in _uiSlots)
                slot.Init(this);
        }
        public virtual void OnBeginDragItem()
        {

        }
    }
}
