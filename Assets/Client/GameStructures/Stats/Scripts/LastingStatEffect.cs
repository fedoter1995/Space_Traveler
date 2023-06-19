using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Stats
{
    public abstract class LastingStatEffect : BaseStat
    {

        protected LastingEffectStatPreset lastingeffectPreset;
        public override void Initialize(StatsHandler handler)
        {
            base.Initialize(handler);

            if (statPreset is null)
                statPreset = lastingeffectPreset;
            else
                lastingeffectPreset = statPreset as LastingEffectStatPreset;
        }
    }
}