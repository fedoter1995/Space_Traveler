using GameStructures.Stats;
using System.Collections.Generic;


namespace GameStructures.Effects
{
    public static  class CritDamage
    {   
        public static HitDamage CalculateValues(HitDamage damage, List<Multiplier> multipliers, List<Chance> chances)
        {
            var resultDamage = new List<DamageTypeValue>();

            foreach (DamageTypeValue dmg in damage.DamageTypeValues)
            {
                var randomValue = UnityEngine.Random.Range(0, 100.1f);


                var multiplier = multipliers.Find(item => item.DamageType == dmg.Type);
                var chance = chances.Find(item => item.EffectRef == multiplier.Preset);
                float mult = 1f;
                float ch = 100f;
                
                if (multiplier != null)
                    mult = multiplier.Value;

                if(chance != null)
                    ch = chance.Value;

                if(ch >= randomValue)
                {
                    var newDamageInt = dmg.Value * mult;

                    var newDamageValue = new DamageTypeValue((int)newDamageInt, dmg.Type, true);

                    resultDamage.Add(newDamageValue);
                }
                else
                {
                    resultDamage.Add(dmg);
                }

            }
            
            HitDamage resultHitDamage = new HitDamage(resultDamage);

            return resultHitDamage;
        }
    }
}

