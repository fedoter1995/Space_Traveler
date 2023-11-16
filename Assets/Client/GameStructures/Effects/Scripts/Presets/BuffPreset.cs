using SpaceTraveler.GameStructures.Stats.StatModifiers;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Effects
{
    [CreateAssetMenu(menuName = "Buffs/New_BuffPreset")]
    public class BuffPreset : ScriptableObject
    {
        [SerializeField]
        private List<StatModifier> _modifiers = new List<StatModifier>();

        public List<StatModifier> Modifiers => _modifiers;
    }
}
