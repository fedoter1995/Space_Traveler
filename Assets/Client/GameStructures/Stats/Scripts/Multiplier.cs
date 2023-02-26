using GameStructures.Effects;
using Stats;
using UnityEngine;

namespace GameStructures.Stats
{
    [System.Serializable]
    public class Multiplier : BaseStat
    {
        [SerializeField]
        private MultiplierPreset _multiplierPreset;
          
        public MultiplierPreset Preset => _multiplierPreset;
        public EffectType EffectType => _multiplierPreset.EffectType;
        public DamageType DamageType => _multiplierPreset.DamageType;

        public override void Initialize(StatsHandler handler)
        {
            base.Initialize(handler);
            if (statPreset is null)
                statPreset = _multiplierPreset;
            else
                _multiplierPreset = statPreset as MultiplierPreset;
        }
    }
}
