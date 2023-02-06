using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [System.Serializable]
    public class StatModifier
    {
        [SerializeField]
        private StatModifierPreset _preset;
        [SerializeField]
        private float _value;

        public StatModifierPreset Preset => _preset;
        public string Name => _preset.Name; 
        public StatModType Type => _preset.Type;
        public List<StatPreset> ZoneOfInfluence => _preset.ZoneOfInfluence;
        public float Value => _value;

        public bool HasInfluenceToStat(string statName)
        {
            var item = ZoneOfInfluence.Find(stat => stat.Name == statName);

            if (item == null)
                return false;

            return true;
        }
        public StatModifier(StatModifierPreset preset, float value)
        {
            _preset = preset;
            _value = value;
        }
    }
}

