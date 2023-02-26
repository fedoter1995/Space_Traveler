using GameStructures.Effects;
using Stats;
using UnityEngine;

namespace GameStructures.Stats
{
    [CreateAssetMenu(menuName = "Stats/New_Multiplier_Preset")]
    public class MultiplierPreset : StatPreset
    {
        [SerializeField]
        private EffectType _type = EffectType.Critical;
        [SerializeField]
        private DamageType _damageType = DamageType.Physical;
        public EffectType EffectType => _type;
        public DamageType DamageType => _damageType;
    }
}
