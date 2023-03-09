using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameStructures.Stats
{
    [CreateAssetMenu(menuName = "Stats/Presets/New_StatModifier_Preset")]
    public class StatModifierPreset : ScriptableObject
    {
        [SerializeField]
        protected string _modifierName = "New Modifire";
        [SerializeField]
        protected string _description;
        [SerializeField]
        private StatModType _type;
        [SerializeField]
        private List<StatPreset> _zoneOfInfluence = new List<StatPreset>();

        public string Name => _modifierName;
        public string Description => _description;
        public StatModType Type => _type;
        public List<StatPreset> ZoneOfInfluence => _zoneOfInfluence;
    }
    public enum StatModType
    {
        Flat,
        PercentAdd,
        PercentMult,
    }
}
