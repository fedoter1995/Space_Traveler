using SpaceTraveler.GameStructures.Stats.Presets;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    [System.Serializable]
    public  class Chance : BaseStat
    {
        [SerializeField]
        private ChancePreset _chancePreset;

        public ChancePreset Preset => _chancePreset;
        public EffectStatPreset EffectRef => _chancePreset.EffectRef;

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

