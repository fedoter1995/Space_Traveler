using SpaceTraveler.GameStructures.Stats.Presets;
using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    [Serializable]
    public class Duration : BaseStat
    {

        [SerializeField]
        private DurationPreset _preset;

        public LastingEffectStatPreset LastingEffectRef => _preset.EffectRef;

    }
}
