using SpaceTraveler.GameStructures.Stats.Presets;
using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    [Serializable]
    public class Resistance : BaseStat
    {
        [SerializeField]
        private ResistancePreset _resistancePreset;
        public DamageType Type => _resistancePreset.Type;
        public override void Initialize()
        {
            base.Initialize();
            if(statPreset is null)
                statPreset = _resistancePreset;
            else
                _resistancePreset = statPreset as ResistancePreset;
        }

    }
}
