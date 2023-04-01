using GameStructures.Gear;
using GameStructures.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Garage.Workshop
{
    [Serializable]
    public class CraftRequirements
    {
        [SerializeField]
        private List<ElementSlot> _elements = new List<ElementSlot>();

        [SerializeField]
        private List<Equipment> _equipments = new List<Equipment>();


        public List<ElementSlot> Elements => _elements;
        public List<Equipment> Equipments => _equipments;
    }
}
