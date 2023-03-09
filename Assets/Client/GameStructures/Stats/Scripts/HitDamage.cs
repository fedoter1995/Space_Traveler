using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStructures.Stats
{
    public class HitDamage
    {
        public Dictionary<DamageType, DamageValue> DamageTypeValueDict { get; private set; }
        public HitDamage(Dictionary<DamageType, DamageValue> damageTypeValueDict)
        {
            DamageTypeValueDict = damageTypeValueDict;
        }
    }

    public struct DamageValue
    {
        public int intNumber { get; }
        public bool isCrit { get; }

        public DamageValue(int value, bool isCrit = false)
        {
            this.intNumber = value;
            this.isCrit = isCrit;
        }

        public override string ToString()
        {
            var str = "";
            if (isCrit)
                str = $"{intNumber} Critical";
            else
                str = $"{intNumber}";
            return str;
        }
    }

}

