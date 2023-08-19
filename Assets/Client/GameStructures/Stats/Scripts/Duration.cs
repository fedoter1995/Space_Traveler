using SpaceTraveler.GameStructures.Stats.Presets;
using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    [Serializable]
    public class Duration : BaseStat
    {

        [SerializeField]
        private DurationPreset _dotPreset;

        public DurationPreset DurationPreset => _dotPreset;

        public override void Initialize()
        {
            base.Initialize();

            if (statPreset is null)
                statPreset = _dotPreset;
            else
                _dotPreset = statPreset as DurationPreset;
        }

    }
}
