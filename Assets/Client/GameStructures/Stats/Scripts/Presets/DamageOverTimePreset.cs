using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats.Presets
{
    [CreateAssetMenu(menuName = "Stats/New_DamageOverTime_Preset")]
    public class DamageOverTimePreset : OverTimeEffectStatPreset
    {
        [SerializeField]
        private DamageType _damageType;
        public DamageType DamageType => _damageType;
    }
}
