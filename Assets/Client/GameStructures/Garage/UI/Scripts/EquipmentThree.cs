using GameStructures.Equipment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentThree : MonoBehaviour
{
    [SerializeField]
    private List<EquipmentBranch> _branches;

    public event Action<Equipment> OnChangeEquipEvent;
    public EquipmentUISlot SlotActual { get; private set; }
    public void SetActive(bool activity)
    {
        gameObject.SetActive(activity);
    }
    public void Initialize(Equipment equip)
    {
        foreach (EquipmentBranch branch in _branches)
        {
            branch.Initialize(this);

            foreach(EquipmentUISlot slot in branch.Slots)
            {
                if (slot.Equip == equip)
                {
                    ChangeActiveEquip(slot);
                }

            }
        }

    }

    public void ChangeActiveEquip(EquipmentUISlot slot)
    {
        if(SlotActual != null)
            SlotActual.SetActive(false);

        SlotActual = slot;

        SlotActual.SetActive(true);

        OnChangeEquipEvent?.Invoke(SlotActual.Equip);
    }

}
