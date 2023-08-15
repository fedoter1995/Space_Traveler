using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Stats.Presets;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Effects
{
    public class DamageOverTime : LastingStatEffect
    {
        [SerializeField]
        private DamageOverTimePreset _dotPreset;

        public DamageOverTimePreset Preset => _dotPreset;

        public DamageType DamageType => _dotPreset.DamageType;

        public override void Initialize(StatsHandler handler)
        {

            base.Initialize(handler);

            if (lastingeffectPreset is null)
                lastingeffectPreset = _dotPreset;
            else
                _dotPreset = lastingeffectPreset as DamageOverTimePreset;
        }
    }
}
