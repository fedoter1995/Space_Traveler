using Assets.Client.GameStructures.Stats.Scripts.Presets;
using SpaceTraveler.GameStructures.Stats.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats.Chances
{
    [Serializable]
    public class MultiplierChance : Chance
    {
        [SerializeField]
        private MultiplierChancePreset _multiplierChancePreset;

        public MultiplierPreset MultiplierRef => _multiplierChancePreset.MultiplierRef;


        public override void Initialize()
        {
            base.Initialize();

            if (statPreset is null)
                statPreset = _multiplierChancePreset;
            else
                _multiplierChancePreset = statPreset as MultiplierChancePreset;
        }

    }
}
