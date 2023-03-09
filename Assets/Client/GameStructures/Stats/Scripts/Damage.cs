using System;
using System.Collections.Generic;
using UnityEngine;
namespace GameStructures.Stats
{
    [Serializable]
    public class Damage : BaseStat
    {
        [SerializeField]
        private DamagePreset _damagePreset;
        public DamageType Type => _damagePreset.Type;

        public override void Initialize(StatsHandler handler)
        {
            base.Initialize(handler);
            if(statPreset is null)
                statPreset = _damagePreset;
            else
                _damagePreset = statPreset as DamagePreset;
        }

    }
    public enum DamageType
    {
        None,
        Enegy,
        Physical
    }
}
