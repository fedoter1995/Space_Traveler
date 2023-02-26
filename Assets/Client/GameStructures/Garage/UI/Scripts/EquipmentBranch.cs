using Garage.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBranch : MonoBehaviour
{
    [SerializeField]
    private List<EquipmentUISlot> _slots;

    public List<EquipmentUISlot> Slots => _slots;

    public event Action<EquipmentUISlot> OnChangeActualSlotEvent;

    public EquipmentThree Three { get; private set; }

    public void Initialize(EquipmentThree three)
    {
        Three = three;
        foreach(EquipmentUISlot slot in _slots)
        {
            slot.Initialize(this);
        }
    }

}
