using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats.Presets
{
    [CreateAssetMenu(menuName = "Stats/Dot/DamageOverTime_Preset")]
    public class DamageOverTimePreset : StatPreset
    {
        [SerializeField]
        private DamageType _damageType;

        public DamageType DamageType => _damageType;
    }
}
