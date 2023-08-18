﻿using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats.Presets
{
    [CreateAssetMenu(menuName = "Stats/New_Duration_Preset")]
    public class DurationPreset : EffectStatPreset
    {
        [SerializeField]
        private LastingEffectStatPreset _effectRef;

        public LastingEffectStatPreset EffectRef => _effectRef;
    }
}
