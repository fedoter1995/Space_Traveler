using GameStructures.Gear.Armors;
using GameStructures.Gear.Engine;
using GameStructures.Gear.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStructures.Gear
{
    [Serializable]
    public class SpaceshipModuleSet
    {
        [SerializeField]
        private SpaceshipWeapon _mainWeapon;
        [SerializeField]
        private MainEngine _engine;
        [SerializeField]
        private ShipSkin _shipSkin;

        public SpaceshipWeapon MainWeapon => _mainWeapon;
        public MainEngine Engine => _engine;
        public ShipSkin ShipSkin => _shipSkin;

        public List<Equipment> GetEquipment()
        {
            var equipment = new List<Equipment>();
            equipment.Add(MainWeapon);
            equipment.Add(Engine);
            equipment.Add(ShipSkin);

            return equipment;
        }
        public void SetEquipment(Equipment equipment)
        {
            var type = equipment.GetType();

            if (type.IsSubclassOf(typeof(SpaceshipWeapon)) || type == typeof(SpaceshipWeapon))
            {
                _mainWeapon = (SpaceshipWeapon)equipment;
                _mainWeapon.InitEquipment();
            }
            else if (type.IsSubclassOf(typeof(ShipSkin)) || type == typeof(ShipSkin))
            {
                _shipSkin = (ShipSkin)equipment;
                _shipSkin.InitEquipment();
            }
            else if (type.IsSubclassOf(typeof(MainEngine)) || type == typeof(MainEngine))
            {
                _engine = (MainEngine)equipment;
                _engine.InitEquipment();
            }

        }
        public void SetEquipment(string id)
        {

            var repository = Architecture.Game.GetRepository<ItemsRepository>();

            var equipment = repository.GetItem<Equipment>(id);

            var type = equipment.GetType();

            if (type.IsSubclassOf(typeof(SpaceshipWeapon)) || type == typeof(SpaceshipWeapon))
            {
                _mainWeapon = (SpaceshipWeapon)equipment;
                _mainWeapon.InitEquipment();
            }
            else if (type.IsSubclassOf(typeof(ShipSkin)) || type == typeof(ShipSkin))
            {
                _shipSkin = (ShipSkin)equipment;
                _shipSkin.InitEquipment();
            }
            else if (type.IsSubclassOf(typeof(MainEngine)) || type == typeof(MainEngine))
            {
                _engine = (MainEngine)equipment;
                _engine.InitEquipment();
            }
        }
    }
}
