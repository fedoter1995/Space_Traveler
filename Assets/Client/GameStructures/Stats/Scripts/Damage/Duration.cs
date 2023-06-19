using System;
using UnityEngine;

namespace GameStructures.Stats
{
    [Serializable]
    public class Duration : BaseStat
    {

        [SerializeField]
        private DurationPreset _preset;

        public LastingEffectStatPreset LastingEffectRef => _preset.EffectRef;

    }
}
