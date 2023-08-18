using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Stats.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    public class MultiplierChance : Chance
    {
        [SerializeField]
        private MultiplierPreset _multiplierPresetRef;

        public MultiplierPreset MultiplierRef => _multiplierPresetRef;

    }
}
