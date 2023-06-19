using GameStructures.Effects;
using UnityEngine;

namespace GameStructures.Stats
{
    [CreateAssetMenu(menuName = "Stats/New_Multiplier_Preset")]
    public class MultiplierPreset : EffectStatPreset
    {
        [SerializeField]
        private MultiplierType _multiplierType = MultiplierType.DamageMultiplier;
        [SerializeField]
        private DamageType _damageType = DamageType.Physical;
        public MultiplierType MultiplierType => _multiplierType;
        public DamageType DamageType => _damageType;
    }
}
