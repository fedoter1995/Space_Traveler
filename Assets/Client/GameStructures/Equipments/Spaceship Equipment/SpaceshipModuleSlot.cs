using SpaceTraveler.GameStructures.Items;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Gear.Spaceship
{
    [System.Serializable]
    public class SpaceshipModuleSlot : IJsonSerializable
    {
        [SerializeField]
        private Equipment _equip;

        public bool IsEmpty => _equip is null;
        public Equipment CurrentItem => _equip;
        public string ItemID => IsEmpty ? "Empty Slot" : _equip.Id;
        public SlotType SlotType => SlotType.Equipment;
        public EquipmentType EquipType => _equip.Type;
        public string Name => _equip.Name;
        public void SetItem(Equipment equip)
        {

            if (equip == null)
            {
                _equip = null;
                return;
            }
     
            if (equip.SlotType == SlotType && equip.Type == EquipType)
                _equip = equip;
        }

        public void Init()
        {
            SetItem(CurrentItem);
        }

        public void SetObjectData(Dictionary<string, object> obj)
        {
        
            if(obj != null)
            {
                var repository = Architecture.Game.GetRepository<ItemsRepository>();
            
                var name = obj["Name"].ToString();
                var id = obj["ID"].ToString();

                SetItem(repository.GetItem<Equipment>(id));
            }
        }

        public Dictionary<string, object> GetObjectData()
        {
            var dict = new Dictionary<string, object>();

            if(!IsEmpty)
            {
                dict.Add("Name", Name);
                dict.Add("ID", ItemID);
            }
            return dict;

        }
    }
}
