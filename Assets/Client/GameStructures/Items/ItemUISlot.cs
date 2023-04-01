using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameStructures.Items
{
    public class ItemUISlot : TooltipUIObject
    {

        [SerializeField]
        private Image _image;
        [SerializeField]
        private Image _stroke;

        private Item item;

        public Item Item => item;

        public bool Availability { get; private set; } = false;

        public void SetItem(Item item, bool availability)
        {
            this.item = item;

            _image.sprite = this.item.Icon;

            Availability = availability;

            SetStrokeColor(availability);

        }

        private void SetStrokeColor(bool availability)
        {
            if (availability)
                _stroke.color = Color.green;
            else
                _stroke.color = Color.red;
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log(this);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            
        }
    }
}

