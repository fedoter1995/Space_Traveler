using System;
using System.Collections.Generic;
using UnityEngine;
namespace Stats
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
            statPreset = _damagePreset;
        }
    }
    public enum DamageType
    {
        Enegy,
        Physical
    }
}
