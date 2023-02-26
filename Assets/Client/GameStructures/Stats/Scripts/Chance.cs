using GameStructures.Effects;
using Stats;
using UnityEngine;

namespace GameStructures.Stats
{
    [System.Serializable]
    public  class Chance : BaseStat
    {
        [SerializeField]
        private ChancePreset _chancePreset;

        public ChancePreset Preset => _chancePreset;
        public EffectType EffectType => _chancePreset.EffectType;
        public DamageType DamageType => _chancePreset.DamageType;

        public override void Initialize(StatsHandler handler)
        {
            base.Initialize(handler);
            if (statPreset is null)
                statPreset = _chancePreset;
            else
                _chancePreset = statPreset as ChancePreset;
        }

    }

}

