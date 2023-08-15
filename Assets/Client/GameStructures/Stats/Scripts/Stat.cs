using SpaceTraveler.GameStructures.Stats.Presets;
using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    [Serializable]
    public class Stat : BaseStat
    {
        [SerializeField]
        protected StatPreset _statPreset;



        public override void Initialize(StatsHandler handler)
        {
            base.Initialize(handler);
            if (statPreset == null)
                statPreset = _statPreset;
            else
                _statPreset = statPreset;
        }

    }
}
