using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStructures.Stats
{
    [CreateAssetMenu(menuName = "Stats/New_Duration_Preset")]
    public class DurationPreset : StatPreset
    {
        [SerializeField]
        private LastingEffectStatPreset _effectRef;

        public LastingEffectStatPreset EffectRef => _effectRef;
    }
}
