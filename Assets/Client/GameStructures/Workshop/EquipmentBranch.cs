using SpaceTraveler.Gear.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Workshop.UI
{
    public class EquipmentBranch : MonoBehaviour
    {
        [SerializeField]
        private List<EquipmentUISlot> _slots;

        public List<EquipmentUISlot> Slots => _slots;

        public event Action<EquipmentUISlot> OnChangeActualSlotEvent;

        public void Initialize()
        {
            foreach(EquipmentUISlot slot in _slots)
            {
                slot.Initialize(this);
                slot.OnClickEvent += OnChangeActualSlot;
            }
        }

        private void OnChangeActualSlot(EquipmentUISlot slot)
        {
            OnChangeActualSlotEvent?.Invoke(slot);
        }
    }
}

