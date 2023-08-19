using UnityEngine;
using System;
using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Stats.Presets;

namespace SpaceTraveler.GameStructures.Stats.Chances
{
    [Serializable]
    public class DotChance : EffectChance
    {
        [SerializeField]
        private DotChancePreset _dotChancePreset;

        public DotChancePreset ChancePreset => _dotChancePreset;


        public override void Initialize()
        {

            if (statPreset is null)
                statPreset = _dotChancePreset;
            else
                _dotChancePreset = statPreset as DotChancePreset;


            if (_dotChancePreset == null)
                throw new Exception("StatPreset cannot be represented as a ActionChancePreset or is null");
        }
    }
}
