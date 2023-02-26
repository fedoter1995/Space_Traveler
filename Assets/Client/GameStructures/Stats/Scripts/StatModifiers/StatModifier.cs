using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [System.Serializable]
    public class StatModifier
    {
        [SerializeField]
        protected StatModifierPreset _preset;
        [SerializeField]
        protected float _value;

        public StatModifierPreset Preset => _preset;
        public string Name => _preset.Name;
        public string Description => _preset.Description;
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
        public List<string> GetDescriptionData()
        {
            var desList = new List<string>();
            var str = $"{Description} {Value}";

            desList.Add(str);

            return desList;
        }
    }
}

