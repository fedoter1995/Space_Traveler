using GameStructures.Gear;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Garage.Workshop
{
    [Serializable]
    public class CraftRequirements
    {
        [SerializeField]
        private List<ItemSlot> _items = new List<ItemSlot>();

        [SerializeField]
        private List<Equipment> _equipments = new List<Equipment>();


        public List<ItemSlot> Items => _items;
        public List<Equipment> Equipments => _equipments;
    }
}
