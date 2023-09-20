using SpaceTraveler.GameStructures.Stats.StatModifiers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Hits
{
    [Serializable]
    public class AddedModifiers
    {
        [SerializeField]
        private List<StatModifier> _modifiers = new List<StatModifier>();

        public List<StatModifier> Modifiers => _modifiers;


        public AddedModifiers(List<StatModifier> modifiers)
        {
            if(_modifiers.Count == 0)
                _modifiers = modifiers;
            else
                _modifiers.AddRange(modifiers);
        }
        public AddedModifiers(StatModifier modifiers)
        {
            _modifiers.Add(modifiers);
        }
    }
}
