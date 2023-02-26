using GameStructures.Stats;
using Stats;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Effects
{
    public static  class CritDamage
    {    
        public static HitDamage CalculateValues(HitDamage damage, List<Chance> chances, List<Multiplier> multipliers)
        {
            var resultDamage = new Dictionary<DamageType, DamageValue>();

            if (chances == null || chances.Count <= 0)
                return damage;
            if(multipliers == null || multipliers.Count <= 0)
                return damage;

            foreach (KeyValuePair<DamageType, DamageValue> entry in damage.DamageTypeValueDict)
            {

                var randomValue = UnityEngine.Random.Range(0, 100.1f);

                var chance = chances.Find(chance => chance.DamageType == entry.Key);

                var mult = multipliers.Find(multiplier => multiplier.DamageType == entry.Key);
                float multiplier = 1f;
                if (mult != null)
                    multiplier = mult.Value;


                if (chance != null && chance.Value >= randomValue)
                {
                    var newDamageInt = entry.Value.intNumber * multiplier;

                    var newDamageValue = new DamageValue((int)newDamageInt, true);

                    resultDamage.Add(entry.Key, newDamageValue);
                }
                else
                    resultDamage.Add(entry.Key, entry.Value);



            }
            
            HitDamage resultHitDamage = new HitDamage(resultDamage);

            return resultHitDamage;
        }
    }
    public enum EffectType
    {
        Critical
    }
}

