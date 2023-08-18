using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Stats.Presets;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Effects
{
    public class DamageOverTime : StatWithAChance
    {
        [SerializeField]
        private DamageOverTimePreset _dotPreset;
        [SerializeField]
        private DotEffectChancePreset _dotChanceRef;
        public DotEffectChancePreset ChanceRef => _dotChanceRef;

        public DamageType DamageType => _dotPreset.DamageType;
        public override void Initialize()
        {
            base.Initialize();

            if (statPreset is null)
                statPreset = _dotPreset;
            else
                _dotPreset = statPreset as DamageOverTimePreset;
        }
    }
}
