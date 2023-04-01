using System;
using System.Collections.Generic;
using CustomTools;
using GameStructures.Gear;
using Newtonsoft.Json.Linq;
using UnityEngine;


namespace GameStructures.Garage.Workshop
{
    [Serializable]
    public class WorkshopSettings : IJsonSerializable
    {
        [SerializeField]
        private List<EquipmentTree> _trees;

        private List<Equipment> _availableEquipment;

        private Spaceship spaceship;

        public List<Equipment> AvailableEquipment
        {
            get
            {
                if (_availableEquipment == null)
                    _availableEquipment = spaceship.Equipment.GetEquipment();

                return _availableEquipment;
            }
        }
        public List<EquipmentTree> Trees => _trees;
        public void Initialize(Spaceship spaceship)
        {
            this.spaceship = spaceship;
        }
        public void AddToAvailableEquipment(Equipment equipment)
        {
            if (_availableEquipment == null)
                _availableEquipment = new List<Equipment>();

            if (!_availableEquipment.Contains(equipment))
                _availableEquipment.Add(equipment);       
        }
        public void SetObjectData(Dictionary<string, object> data)
        {
            var array = (JArray)data["AvailableEquipment"];

            var equipmentData = CustomConvert.JArrayToList<Dictionary<string, object>>(array);

            var repository = Architecture.Game.GetRepository<ItemsRepository>();
            
            _availableEquipment = new List<Equipment>();

            foreach (Dictionary<string,object> equipData in equipmentData)
            {
                var equipment = repository.GetItem<Equipment>(equipData["Id"].ToString());

                _availableEquipment.Add(equipment);

            }

        }
        public Dictionary<string, object> GetObjectData()
        {
            var data = new Dictionary<string, object>();
            var dataList = new List<Dictionary<string,object>>();
            foreach(Equipment equip in AvailableEquipment)
            {
                dataList.Add(equip.GetObjectData());
            }

            data.Add("AvailableEquipment", dataList);

            return data;
        }
        public bool HasEquipment(string id)
        {
            var item = _availableEquipment.Find(item => item.Id == id);
            if (item != null)
                return true;

            return false;
        }
        public override string ToString()
        {
            return "WorkshopSettings";
        }
    }
}

