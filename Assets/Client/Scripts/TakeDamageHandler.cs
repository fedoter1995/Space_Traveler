using System;
using Stats;
using System.Linq;
using System.Collections.Generic;

namespace Stats
{
    public static class TakeDamageHandler
    {

        public static int CalculateDamage(ShotDamage damage,List<Resistance> resistances)
        {
            int resultDamage = 0;
            foreach (KeyValuePair<DamageType, float> entry in damage.DamageTypeValueDict)
            {
                var res = resistances.Find(res => res.Type == entry.Key);
                if (res != null)
                    resultDamage += (int)(entry.Value - (entry.Value / 100 * res.Value));
                else
                    resultDamage += (int)entry.Value;
            }

            if (resultDamage < 0)
                return 0;

            return resultDamage;
        }
    }
}


