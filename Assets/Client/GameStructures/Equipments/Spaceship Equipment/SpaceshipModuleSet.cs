using SpaceTraveler.GameStructures.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Gear.Spaceship
{
    [Serializable]
    public class SpaceshipModuleSet : IEqupmentModuleSet<SpaceshipEquipment>
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

        public List<SpaceshipEquipment> GetEquipment()
        {
            var equipment = new List<SpaceshipEquipment>();
            equipment.Add(MainWeapon);
            equipment.Add(Engine);
            equipment.Add(ShipSkin);

            return equipment;
        }

        public bool TrySetEquipment(SpaceshipEquipment equipment)
        {
            if (equipment != null)
            {
                SetEquipment(equipment);
                return true;
            }

            return false;
        }

        public bool TrySetEquipment(string id)
        {
            var repository = Architecture.Game.GetRepository<ItemsRepository>();

            var equipment = repository.GetItem<SpaceshipEquipment>(id);

            if (equipment != null)
            {
                SetEquipment(equipment);
                return true;
            }

            return false;
        }

        private void SetEquipment(SpaceshipEquipment equipment)
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



    }
}
