using SpaceTraveler.GameStructures.Stats.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats.Chances
{
    public class MultiplierChance : Chance
    {
        [SerializeField]
        private MultiplierPreset _multiplierPresetRef;

        public MultiplierPreset MultiplierRef => _multiplierPresetRef;

        public override void GetSuitableStats(StatsHandler handler)
        {
            throw new NotImplementedException();
        }
    }
}
