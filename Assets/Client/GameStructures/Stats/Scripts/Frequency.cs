using SpaceTraveler.GameStructures.Stats.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    [Serializable]
    public class Frequency : BaseStat
    {
        [SerializeField]
        private FrequencyPreset _preset;

        public FrequencyPreset Preset => _preset;

        public override void Initialize()
        {
            base.Initialize();

            if (statPreset is null)
                statPreset = _preset;
            else
                _preset = statPreset as FrequencyPreset;
        }
    }
}
