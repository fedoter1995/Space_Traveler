using Assets.Client.GameStructures.Stats.Scripts.Presets;
using SpaceTraveler.GameStructures.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats.Presets
{
    [CreateAssetMenu(menuName = "Stats/Dot/Chance_Preset")]
    public class DotChancePreset : EffectChancePreset
    {
        [SerializeField]
        private DamageOverTimePreset _damageOverTimeRef;
        [SerializeField]
        private DurationPreset _dotDurationRef;
        [SerializeField]
        private FrequencyPreset _frequencyRef;

        public DamageOverTimePreset DamageOverTimeRef => _damageOverTimeRef;
        public DurationPreset DamageOverTimeDurationRef => _dotDurationRef;
        public FrequencyPreset FrequencyRef => _frequencyRef;

    }
}
