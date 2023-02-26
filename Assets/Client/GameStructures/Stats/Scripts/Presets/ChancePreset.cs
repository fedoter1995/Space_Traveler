using GameStructures.Effects;
using Stats;
using UnityEngine;


namespace GameStructures.Stats
{
    [CreateAssetMenu(menuName = "Stats/New_Chance_Preset")]
    public class ChancePreset : StatPreset
    {
        [SerializeField]
        private EffectType _type = EffectType.Critical;
        [SerializeField]
        private DamageType _damageType = DamageType.None;
        public EffectType EffectType => _type;
        public DamageType DamageType => _damageType;
    }
}
