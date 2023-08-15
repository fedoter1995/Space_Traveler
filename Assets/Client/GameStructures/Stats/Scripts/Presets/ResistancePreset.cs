using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats.Presets
{
    [CreateAssetMenu(menuName = "Stats/Presets/New_Resistance_Preset")]
    public class ResistancePreset : StatPreset
    {
        [SerializeField]
        private DamageType _type;
        public DamageType Type => _type;
    }
}
