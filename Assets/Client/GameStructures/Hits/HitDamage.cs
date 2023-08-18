using System;
using System.Collections.Generic;

namespace SpaceTraveler.GameStructures.Stats
{
    [Serializable]
    public class HitDamage
    {
        public List<DamageAttributes> DamageTypeValues { get; private set; }
        public HitDamage(List<DamageAttributes> damageTypeValues)
        {
            DamageTypeValues = damageTypeValues;
        }
        public HitDamage(DamageAttributes damageTypeValue)
        {
            DamageTypeValues = new List<DamageAttributes>
            {
                damageTypeValue
            };
        }

        public bool IsZeroValue()
        {
            var damageValue = 0;
            foreach (DamageAttributes dmg in DamageTypeValues)
            {
                damageValue += dmg.Value;
            }

            return damageValue == 0;
        }
    }

}

