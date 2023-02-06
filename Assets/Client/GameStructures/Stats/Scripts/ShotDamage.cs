using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stats
{
    public class ShotDamage
    {
        public Dictionary<DamageType, float> DamageTypeValueDict { get; private set; }
        public ShotDamage(Dictionary<DamageType, float> damageTypeValueDict)
        {
            DamageTypeValueDict = damageTypeValueDict;
        }
    }
}

