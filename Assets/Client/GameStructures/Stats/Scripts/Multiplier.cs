using SpaceTraveler.GameStructures.Stats.Presets;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    [System.Serializable]
    public class Multiplier : BaseStat
    {
        [SerializeField]
        private MultiplierPreset _multiplierPreset;
         
        public MultiplierPreset Preset => _multiplierPreset;
        public MultiplierType MultiplierType => _multiplierPreset.MultiplierType;
        public DamageType DamageType => _multiplierPreset.DamageType;

        public override void Initialize()
        {
            base.Initialize();

            if (statPreset is null)
                statPreset = _multiplierPreset;
            else
                _multiplierPreset = statPreset as MultiplierPreset;
        }
    }
    public enum MultiplierType
    {
        DamageMultiplier,
        DotMultiplier,
    }
}
