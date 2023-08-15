using SpaceTraveler.GameStructures.Gear;
using SpaceTraveler.GameStructures.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Craft
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
