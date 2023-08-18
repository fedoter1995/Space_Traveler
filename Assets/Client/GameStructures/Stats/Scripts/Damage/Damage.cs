using SpaceTraveler.GameStructures.Stats.Presets;
using System;
using UnityEngine;
namespace SpaceTraveler.GameStructures.Stats
{
    [Serializable]
    public class Damage : BaseStat
    {
        [SerializeField]
        private DamagePreset _damagePreset;
        public DamageType Type => _damagePreset.Type;

        public override void Initialize()
        {
            base.Initialize();
            if(statPreset is null)
                statPreset = _damagePreset;
            else
                _damagePreset = statPreset as DamagePreset;
        }

    }
    public enum DamageType
    {
        Fire,
        Cold,
        Electro,
        Enegy,
        Physical
    }
}
