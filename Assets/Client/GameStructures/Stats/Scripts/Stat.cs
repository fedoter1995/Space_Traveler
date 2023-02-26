using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class Stat : BaseStat
    {
        [SerializeField]
        protected StatPreset _statPreset;



        public override void Initialize(StatsHandler handler)
        {
            base.Initialize(handler);
            if (statPreset == null)
                statPreset = _statPreset;
            else
                _statPreset = statPreset;
        }

    }
}
