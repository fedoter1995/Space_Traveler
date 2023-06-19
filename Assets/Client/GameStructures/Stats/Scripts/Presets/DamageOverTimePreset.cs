using GameStructures.Stats;
using UnityEngine;

namespace GameStructures.Stats
{
    [CreateAssetMenu(menuName = "Stats/New_DamageOverTime_Preset")]
    public class DamageOverTimePreset : OverTimeEffectStatPreset
    {
        [SerializeField]
        private DamageType _damageType;
        public DamageType DamageType => _damageType;
    }
}
