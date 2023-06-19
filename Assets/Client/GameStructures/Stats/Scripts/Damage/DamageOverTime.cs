using GameStructures.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStructures.Effects
{
    public class DamageOverTime : LastingStatEffect
    {
        [SerializeField]
        private DamageOverTimePreset _dotPreset;

        public DamageOverTimePreset Preset => _dotPreset;

        public DamageType DamageType => _dotPreset.DamageType;

        public override void Initialize(StatsHandler handler)
        {

            base.Initialize(handler);

            if (lastingeffectPreset is null)
                lastingeffectPreset = _dotPreset;
            else
                _dotPreset = lastingeffectPreset as DamageOverTimePreset;
        }
    }
}
