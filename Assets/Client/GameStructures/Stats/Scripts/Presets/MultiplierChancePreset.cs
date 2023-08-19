using SpaceTraveler.GameStructures.Stats.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats.Presets
{
    public class MultiplierChancePreset : ChancePreset
    {
        [SerializeField]
        private MultiplierPreset _multiplierRef;

        public MultiplierPreset MultiplierRef => _multiplierRef;
    }
}
