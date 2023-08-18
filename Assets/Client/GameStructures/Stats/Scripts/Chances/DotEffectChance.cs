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
        private DotEffectChancePreset _dotEffect;

        public DOTEffect Effect => _dotEffect;

        public override void GetSuitableStats(StatsHandler handler)
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {

            if (statPreset is null)
                statPreset = _actionChancePreset;
            else
                _actionChancePreset = statPreset as ActionChancePreset;


            if (_actionChancePreset == null)
                throw new Exception("StatPreset cannot be represented as a ActionChancePreset or is null");
        }
    }
}
