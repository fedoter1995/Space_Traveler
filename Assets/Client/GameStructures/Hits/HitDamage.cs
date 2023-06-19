using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStructures.Stats
{
    [Serializable]
    public class HitDamage
    {
        public List<DamageTypeValue> DamageTypeValues { get; private set; }
        public HitDamage(List<DamageTypeValue> damageTypeValues)
        {
            DamageTypeValues = damageTypeValues;
        }
        public HitDamage(DamageTypeValue damageTypeValue)
        {
            DamageTypeValues = new List<DamageTypeValue>();
            DamageTypeValues.Add(damageTypeValue);
        }
    }
    [Serializable]
    public struct DamageTypeValue
    {
        [SerializeField]
        private int value;
        [SerializeField]
        private bool isCrit;
        [SerializeField]
        private DamageType type;

        public int Value => value;
        public bool IsCrit => isCrit;
        public DamageType Type => type;

        public DamageTypeValue(int value, DamageType type, bool isCrit = false)
        {
            this.value = value;
            this.type = type;
            this.isCrit = isCrit;
        }

        public override string ToString()
        {
            var str = "";
 
            str = $"{Value} - {Type} damage";

            if (IsCrit)
                str += " Critical";

            return str;
        }
    }

}

