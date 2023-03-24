using System;
using System.Collections.Generic;
using UnityEngine;
using GameStructures.Gear.Weapons;
using GameStructures.Gear.Armors;
using GameStructures.Gear.Engine;
using GameStructures.Stats;

namespace GameStructures.Gear
{
    [Serializable]
    public class EquipmentHandler : IJsonSerializable
    {
        [SerializeField]
        private SpaceshipEquipmentSet _equipmentSet;

        public Weapon MainWeapon => _equipmentSet.MainWeapon;
        public MainEngine Engine => _equipmentSet.Engine;
        public ShipSkin ShipSkin => _equipmentSet.ShipSkin;

        public event Action OnEquipmentChangeEvent;

        public List<StatModifier> GetAllModifiers()
        {
            List<StatModifier> modifiers = new List<StatModifier>();


            modifiers.AddRange(MainWeapon.GetAllModifiers());
            modifiers.AddRange(Engine.GetAllModifiers());
            modifiers.AddRange(ShipSkin.GetAllModifiers());
            return modifiers;
        }

        public void Initialize()
        {
            ShipSkin.InitEquipment();
            Engine.InitEquipment();
            MainWeapon.InitEquipment();
        }
        public List<Equipment> GetEquipment()
        {
            var equipment = new List<Equipment>();

            equipment.Add(MainWeapon);
            equipment.Add(Engine);
            equipment.Add(ShipSkin);

            return equipment;
        }

        public void SetObjectData(Dictionary<string, object> obj)
        {
            if (_equipmentSet == null)
                _equipmentSet = new SpaceshipEquipmentSet();

            if(obj != null)
            {
                SetEquipment(obj["Main_Weapon"].ToString());
                SetEquipment(obj["Main_Engine"].ToString());
                SetEquipment(obj["Ship_Skin"].ToString());
            }

            Initialize();
        }
        public Dictionary<string, object> GetObjectData()
        {
            var data = new Dictionary<string, object>();


            data.Add("Main_Weapon", MainWeapon.Id);
            data.Add("Main_Engine", Engine.Id);
            data.Add("Ship_Skin", ShipSkin.Id);

            return data;
        }
        public void SetEquipment(Equipment equipment)
        {
            _equipmentSet.SetEquipment(equipment);
            OnEquipmentChangeEvent?.Invoke();
        }
        public void SetEquipment(string id)
        {
            _equipmentSet.SetEquipment(id);
        }
        public override string ToString()
        {
            return "Spaceship Equipment";
        }
    }
}


