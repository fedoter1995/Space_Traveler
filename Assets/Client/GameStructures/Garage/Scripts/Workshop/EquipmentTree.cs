using GameStructures.Gear;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Attributes;

public class EquipmentTree : MonoBehaviour
{
    [SerializeField]
    private string _name = "EquipmentTree";
    [SerializeField, ClassReference(typeof(Equipment))]
    private string _type;
    [SerializeField]
    private List<EquipmentBranch> _branches;

    public event Action<EquipmentUISlot> OnChangeSlotEvent;
    public event Action<Equipment> OnChangeEqipmentEvent;
    public string Name => _name;
    public string TypeReference => _type;
    public EquipmentUISlot SlotActual { get; private set; }
    public Equipment EquipActual { get; private set; }
    public void SetActive(bool activity)
    {
        gameObject.SetActive(activity);
    }
    public void Initialize(Equipment equip, List<Equipment> availableEquipment)
    {
        foreach (EquipmentBranch branch in _branches)
        {
            branch.Initialize();

            branch.OnChangeActualSlotEvent += ChangeActiveSlot;

            foreach(EquipmentUISlot slot in branch.Slots)
            {
                if (slot.Equip == equip)
                {
                    Debug.Log(equip);
                    ChangeActiveSlot(slot);
                    EquipActual = slot.Equip;
                }
           

                bool availability = availableEquipment.Contains(slot.Equip);
                slot.SetAvailability(availability);
            }
        }

    }
    public void ChangeActiveSlot(EquipmentUISlot slot)
    {
        if(SlotActual != null)
            SlotActual.SetActive(false);

        SlotActual = slot;

        SlotActual.SetActive(true);

        OnChangeSlotEvent?.Invoke(SlotActual);
    }
    public void SetActualEquipment(Equipment equipment)
    {
        EquipActual = equipment;
        OnChangeEqipmentEvent.Invoke(EquipActual);
    }

}
