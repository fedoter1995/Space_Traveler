using System;
using System.Collections.Generic;
using UnityEngine;
using SpaceTraveler.GameStructures.Stats.StatModifiers;

namespace SpaceTraveler.GameStructures.Gear.Spaceship
{
    [Serializable]
    public class SpaceshipModuleHandler : IJsonSerializable, IEqupmentHandler
    {
        [SerializeField]
        private SpaceshipModuleSet _equipmentSet;

        public SpaceshipWeapon MainWeapon => _equipmentSet.MainWeapon;
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

        public bool TrySetEquipment(Equipment equipment)
        {
            var spaceEquipment = equipment as SpaceshipEquipment;
            if (spaceEquipment != null)
            {
                _equipmentSet.TrySetEquipment(equipment as SpaceshipEquipment);
                OnEquipmentChangeEvent?.Invoke();
                return true;
            }
            return false;
        }

        public bool TrySetEquipment(string id)
        {
            if (_equipmentSet.TrySetEquipment(id))
            {
                OnEquipmentChangeEvent?.Invoke();
                return true;
            }
            return false;
        }
        public void SetObjectData(Dictionary<string, object> obj)
        {
            if (_equipmentSet == null)
                _equipmentSet = new SpaceshipModuleSet();

            if (obj != null)
            {
                TrySetEquipment(obj["Main_Weapon"].ToString());
                TrySetEquipment(obj["Main_Engine"].ToString());
                TrySetEquipment(obj["Ship_Skin"].ToString());
            }
        }
        public Dictionary<string, object> GetObjectData()
        {
            var data = new Dictionary<string, object>();


            data.Add("Main_Weapon", MainWeapon.Id);
            data.Add("Main_Engine", Engine.Id);
            data.Add("Ship_Skin", ShipSkin.Id);

            return data;
        }
        public override string ToString()
        {
            return "Spaceship Equipment";
        }
    }
}


