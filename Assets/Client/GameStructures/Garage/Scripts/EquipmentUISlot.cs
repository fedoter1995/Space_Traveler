using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SpaceTraveler.GameStructures.Gear;
using SpaceTraveler.GameStructures.Workshop.UI;
using SpaceTraveler.UI;

namespace SpaceTraveler.Gear.UI
{
    [ExecuteInEditMode]
    public class EquipmentUISlot : TooltipUIObject, IUIEquipmentSlot
    {
        [SerializeField]
        private Equipment _equip;


        [SerializeField]
        private Image _image;
        [SerializeField]
        private Image _stroke;
        [SerializeField]
        private Image _hideFilter;

        public bool Availability { get; private set; }

        public event Action<EquipmentUISlot> OnClickEvent;

        public EquipmentBranch Branch { get; private set; }
        public Equipment Equip => _equip;

        public void Initialize(EquipmentBranch branch)
        {
            Branch = branch;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickEvent?.Invoke(this);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            //ShowDescription(eventData);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            //tooltip.HideTooltip();
        }


        public void SetActive(bool activity)
        {
            _stroke.gameObject.SetActive(activity);
        }
        public void SetAvailability(bool availability)
        {
            _hideFilter.gameObject.SetActive(!availability);
            Availability = availability;
        }

        void OnGUI()
        {
            if (_image != null && _equip != null)
                if (_image.sprite != _equip.Icon)
                    _image.sprite = _equip.Icon;
        }

    }
}

