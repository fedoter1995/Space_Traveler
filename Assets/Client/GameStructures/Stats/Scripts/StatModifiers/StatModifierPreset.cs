using SpaceTraveler.GameStructures.Stats.Presets;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats.StatModifiers
{
    [CreateAssetMenu(menuName = "Stats/Presets/New_StatModifier_Preset")]
    public class StatModifierPreset : ScriptableObject
    {
        [SerializeField]
        protected string _modifierName = "New Modifire";
        [SerializeField]
        protected string _description;
        [SerializeField]
        private StatModifierType _type;
        [SerializeField]
        private List<StatPreset> _zoneOfInfluence = new List<StatPreset>();

        public string Name => _modifierName;
        public string Description => _description;
        public StatModifierType Type => _type;
        public List<StatPreset> ZoneOfInfluence => _zoneOfInfluence;
    }
    public enum StatModifierType
    {
        FlatAdd,
        Multiplier,
        PercentAdd,

    }
}
