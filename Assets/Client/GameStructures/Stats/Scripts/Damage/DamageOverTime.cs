using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Stats.Presets;
using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Effects
{
    [Serializable]
    public class DamageOverTime : StatWithAChance
    {
        [SerializeField]
        private DamageOverTimePreset _dotPreset;
        public DamageOverTimePreset DamageOverTimeRef => _dotPreset;

        public DamageType DamageType => DamageOverTimeRef.DamageType;
        public override void Initialize()
        {
            base.Initialize();

            if (statPreset is null)
                statPreset = DamageOverTimeRef;
            else
                _dotPreset = statPreset as DamageOverTimePreset;
        }
    }
}
