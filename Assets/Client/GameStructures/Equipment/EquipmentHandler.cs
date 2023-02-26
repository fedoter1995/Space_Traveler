using System;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using Newtonsoft.Json.Linq;

[Serializable]
public class EquipmentHandler : IJsonSerializable
{
    [SerializeField]
    private List<EquipmentSlot> _equipmentSlots = new List<EquipmentSlot>();
    [SerializeField]
    private Weapon _mainWeapon;
    [SerializeField]
    private Armor _mainArmor;


    public event Action OnItemStateChangedEvent;

    public Weapon MainWeapon => _mainWeapon;
    public Armor MainArmor => _mainArmor;



    public List<StatModifier> GetAllModifiers()
    {
        List<StatModifier> modifiers = new List<StatModifier>();

        foreach (EquipmentSlot slot in _equipmentSlots)
        {
            var equip = slot.CurrentItem;

                if (equip != null)
                    modifiers.AddRange(equip.GetAllModifiers());
        }
        modifiers.AddRange(_mainWeapon.GetAllModifiers());
        return modifiers;
    }

    public void EquipmentInitialize()
    {
        foreach(EquipmentSlot slot in _equipmentSlots)
        {
            if(!slot.IsEmpty)
                slot.CurrentItem.InitEquipment();
        }
        _mainWeapon.InitEquipment();
    }
    public List<Equipment> GetEquipment()
    {
        var equipment = new List<Equipment>();

        equipment.Add(MainWeapon);

        return equipment;
    }
    public void SetEquipment(Equipment equipment)
    {
        var type = equipment.GetType();

        
        if ((Weapon)equipment != null)
            _mainWeapon = (Weapon)equipment;

        EquipmentInitialize();
    }
    public void SetObjectData(Dictionary<string, object> obj)
    {
        if(obj != null)
        {
            JObject slotsJobj = (JObject)obj["Equipment"];
            var equipData = slotsJobj.ToObject<Dictionary<string, Dictionary<string, object>>>();
            var newEquipment = new List<EquipmentSlot>();

            foreach (KeyValuePair<string, Dictionary<string, object>> entry in equipData)
            {
                var slot = new EquipmentSlot();

                slot.SetObjectData(entry.Value);

                newEquipment.Add(slot);
            }
            SetWeapon(obj["Main_Weapon"].ToString());
            _equipmentSlots = new List<EquipmentSlot>(newEquipment);
        }
        
    }

    public Dictionary<string, object> GetObjectData()
    {
        var dict = new Dictionary<string, object>();
        var equip = new Dictionary<string,object>();

        for(int i = 0; i < _equipmentSlots.Count; i++)
        {
            equip.Add($"Slot {i}", _equipmentSlots[i].GetObjectData());
        }

        dict.Add("Main_Weapon", _mainWeapon.Id);

        dict.Add("Equipment", equip);

        return dict;
    }

    private void SetWeapon(string id)
    {

        var repository = Architecture.Game.GetRepository<ItemsRepository>();
        Item weapon = repository.GetItem(id);

        _mainWeapon = weapon as Weapon;
        
    }
    public override string ToString()
    {
        return "Spaceship Equipment";
    }
}

