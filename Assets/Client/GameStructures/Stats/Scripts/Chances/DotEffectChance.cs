using UnityEngine;
using System;
using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Stats.Presets;

namespace SpaceTraveler.GameStructures.Stats.Chances
{
    [Serializable]
    public class DotEffectChance : EffectChance
    {
        [SerializeField]
        private DotEffectChancePreset _dotChancePreset;

        public DotEffectChancePreset ChancePreset => _dotChancePreset;

        public override void GetSuitableStats(StatsHandler handler)
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {

            if (statPreset is null)
                statPreset = _dotChancePreset;
            else
                _dotChancePreset = statPreset as DotEffectChancePreset;


            if (_dotChancePreset == null)
                throw new Exception("StatPreset cannot be represented as a ActionChancePreset or is null");
        }
    }
}
