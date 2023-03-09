using System;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using GameStructures.Stats;

namespace GameStructures.Equipment
{
    public abstract class Equipment : Item
    {
        [SerializeField]
        protected List<StatModifier> _modifiers = new List<StatModifier>();
        [SerializeField]
        protected EquipmentType _equipType;

        public EquipmentType Type => _equipType;
        public abstract void InitEquipment();
        public virtual List<StatModifier> GetAllModifiers()
        {
            return new List<StatModifier>(_modifiers);
        }
    }
    public enum EquipmentType
    {
        Weapon,
        Armor,
        MainEngine,
    }
}
