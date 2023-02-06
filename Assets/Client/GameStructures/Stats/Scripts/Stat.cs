using System;
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
            statPreset = _statPreset;
        }
    }
}
