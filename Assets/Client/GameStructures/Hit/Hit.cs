using GameStructures.Stats;
using GameStructures.Effects;
using Stats;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Hit
{
    public class Hit
    {
        public HitStats Stats { get; private set; }

        
        public Hit(HitStats stats)
        {
            Stats = stats;
        }

        public HitDamage GetHitDamage(List<Resistance> resistances)
        {
            HitDamage takenDamage = CalculateDamage(Stats.ShotDamage, resistances);

            return takenDamage;
        }
        private HitDamage CalculateDamage(HitDamage damage, List<Resistance> resistances)
        {
            var critChances = new List<Chance>(Stats.Chances.FindAll(chance => chance.EffectType == EffectType.Critical));
            var critMultipliers = new List<Multiplier>(Stats.Multipliers.FindAll(multiplier => multiplier.EffectType == EffectType.Critical));
            var resultDamage = CritDamage.CalculateValues(damage,critChances, critMultipliers);

            resultDamage = ApplyResistances(resultDamage, resistances);

            return resultDamage;
        }
        private HitDamage ApplyResistances(HitDamage damage, List<Resistance> resistances)
        {
            var resultDictionary = new Dictionary<DamageType, DamageValue>();

            foreach (KeyValuePair<DamageType, DamageValue> entry in damage.DamageTypeValueDict)
            {
                var res = resistances.Find(res => res.Type == entry.Key);

                if (res != null)
                {
                    var resultIntDamage = (int)(entry.Value.intNumber - entry.Value.intNumber * (res.Value / 100));
                    var newDamageValue = new DamageValue(resultIntDamage, entry.Value.isCrit);

                    resultDictionary.Add(entry.Key, newDamageValue);
                }
                else
                    resultDictionary.Add(entry.Key, entry.Value);
            }
            var newHitDamage = new HitDamage(resultDictionary);


            return newHitDamage;
        }

    }
}
