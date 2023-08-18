using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Stats.Chances;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Hits
{
    [Serializable]
    public class AddedModifiers
    {
        [SerializeField]
        private List<DamageAttributes> _damages = new List<DamageAttributes>();
        [SerializeField]
        private List<Multiplier> _multipliers = new List<Multiplier>();
        [SerializeField]
        private List<MultiplierChance> _multChances = new List<MultiplierChance>();
        [SerializeField]
        private List<Effect> _effects = new List<Effect>();

        public List<DamageAttributes> AddedDamages => _damages;
        public List<Multiplier> AddedMultipliers => _multipliers;
        public List<MultiplierChance> Multiplierhances => _multChances;
        private List<Effect> Effects => _effects;


        public AddedModifiers(List<DamageAttributes> damages = null, List<Multiplier> multipliers = null, List<MultiplierChance> chances = null, List<Effect> effects = null)
        {
            if (damages != null)
                _damages = damages;
            if (multipliers != null)
                _multipliers = multipliers;
            if (chances != null)
                _multChances = chances;
            if (effects != null)
                _effects = effects;
        }
    }
}
