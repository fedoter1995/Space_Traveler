using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Stats
{
    [CreateAssetMenu(menuName = "Stats/Presets/New_StatModifier_Preset")]
    public class StatModifierPreset : ScriptableObject
    {
        [SerializeField]
        protected string _modifierName = "New Modifire";
        [SerializeField]
        private StatModType _type;
        [SerializeField]
        private List<StatPreset> _zoneOfInfluence = new List<StatPreset>();

        public string Name => _modifierName;
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
