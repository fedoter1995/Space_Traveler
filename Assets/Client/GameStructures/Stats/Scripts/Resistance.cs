using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class Resistance : BaseStat
    {
        [SerializeField]
        private ResistancePreset _resistancePreset;
        public DamageType Type => _resistancePreset.Type;
        public override void Initialize(StatsHandler handler)
        {
            base.Initialize(handler);
            statPreset = _resistancePreset;
        }

    }
}
