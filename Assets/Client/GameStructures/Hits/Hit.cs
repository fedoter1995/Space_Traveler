using GameStructures.Stats;
using GameStructures.Effects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Hits
{
    public class Hit
    {
        public HitStats Stats { get; private set; }

        
        public Hit(HitStats stats)
        {
            Stats = stats;
        }

        public HitDamage GetHitDamage()
        {
            HitDamage takenDamage = CalculateDamage(Stats.ShotDamage);

            return takenDamage;
        }
        private HitDamage CalculateDamage(HitDamage damage)
        {

            var multipliers = Stats.Multipliers.FindAll(multiplier => multiplier.MultiplierType == MultiplierType.DamageMultiplier);
            var chances = new List<Chance>(Stats.Chances);

            var resultDamage = CritDamage.CalculateValues(damage, multipliers, chances);

            return resultDamage;
        }

    }
}
