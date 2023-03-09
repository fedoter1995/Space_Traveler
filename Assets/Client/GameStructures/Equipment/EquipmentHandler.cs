using System;
using System.Collections.Generic;
using UnityEngine;
using GameStructures.Equipment.Weapons;
using GameStructures.Equipment.Armors;
using GameStructures.Equipment.Engine;
using GameStructures.Stats;

namespace GameStructures.Equipment
{
    [Serializable]
    public class EquipmentHandler : IJsonSerializable
    {
        [SerializeField]
        private Weapon _mainWeapon;
        [SerializeField]
        private MainEngine _engine;
        [SerializeField]
        private ShipSkin _shipSkin;


        public Weapon MainWeapon => _mainWeapon;
        public MainEngine Engine => _engine;
        public ShipSkin Armor => _shipSkin;



        public List<StatModifier> GetAllModifiers()
        {
            List<StatModifier> modifiers = new List<StatModifier>();


            modifiers.AddRange(_mainWeapon.GetAllModifiers());
            modifiers.AddRange(_engine.GetAllModifiers());
            modifiers.AddRange(_shipSkin.GetAllModifiers());
            return modifiers;
        }

        public void EquipmentInitialize()
        {
            _shipSkin.InitEquipment();
            _engine.InitEquipment();
            _mainWeapon.InitEquipment();
        }
        public List<Equipment> GetEquipment()
        {
            var equipment = new List<Equipment>();

            equipment.Add(MainWeapon);
            equipment.Add(Engine);
            equipment.Add(Armor);

            return equipment;
        }

        public void SetObjectData(Dictionary<string, object> obj)
        {
            if(obj != null)
            {
                SetEquipment(obj["Main_Weapon"].ToString());
                SetEquipment(obj["Main_Engine"].ToString());
                SetEquipment(obj["Ship_Skin"].ToString());
            }
            EquipmentInitialize();
        }
        public Dictionary<string, object> GetObjectData()
        {
            var data = new Dictionary<string, object>();


            data.Add("Main_Weapon", _mainWeapon.Id);
            data.Add("Main_Engine", _engine.Id);
            data.Add("Ship_Skin", _shipSkin.Id);

            return data;
        }
        public void SetEquipment(Equipment equipment)
        {
            var type = equipment.GetType();

            if (type.IsSubclassOf(typeof(Weapon)))
            {
                _mainWeapon = (Weapon)equipment;
                _mainWeapon.InitEquipment();
                return;
            }


            if (type.IsSubclassOf(typeof(ShipSkin)))
            {
                _shipSkin = (ShipSkin)equipment;
                _shipSkin.InitEquipment();
                return;
            }


            if (type.IsSubclassOf(typeof(MainEngine)))
            {
                _engine = (MainEngine)equipment;
                _engine.InitEquipment();
                return;
            }

        }
        public void SetEquipment(string id)
        {

            var repository = Architecture.Game.GetRepository<ItemsRepository>();


            var equipment = (Equipment)repository.GetItem(id);

            var type = equipment.GetType();

            if (type.IsSubclassOf(typeof(Weapon)))
                _mainWeapon = (Weapon)equipment;

                
            if (type == typeof(ShipSkin))
                _shipSkin = (ShipSkin)equipment;


            if (type == typeof(MainEngine))
                _engine = (MainEngine)equipment;
             

        }
        public override string ToString()
        {
            return "Spaceship Equipment";
        }
    }
}


