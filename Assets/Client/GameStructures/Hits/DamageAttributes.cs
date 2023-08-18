using System;
using UnityEngine;
using UnityEngine.Analytics;

namespace SpaceTraveler.GameStructures.Stats
{

    [Serializable]
    public class DamageAttributes
    {
        [SerializeField]
        private int _value;
        [SerializeField]
        private bool _isCrit;
        [SerializeField]
        private DamageType _type;


        public int Value => _value;
        public bool IsCrit => _isCrit;
        public DamageType Type => _type;

        public DamageAttributes(int value, DamageType type, bool isCrit = false)
        {    
            _value = value;
            _type = type;
            _isCrit = isCrit;

        }

        public override string ToString()
        {
            var str = "";
 
            str = $"Received {Value} {Type} damage";

            if (IsCrit)
                str += " - Critical";

            return str;
        }
    }
    
    [Serializable]
    public class HealAttributes
    {
        [SerializeField]
        private int _value;
        [SerializeField]
        private bool _isCrit;

        public int Value => _value;
        public bool IsCrit => _isCrit;

        public HealAttributes(int value, bool isCrit = false)
        {
            _value = value;
            _isCrit = isCrit;
        }

        public override string ToString()
        {
            var str = "";

            str = $"Received {Value} health";

            if (IsCrit)
                str += " - Critical";

            return str;
        }
    }
}

