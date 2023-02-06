using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [System.Serializable]
    public abstract class BaseStat
    {
        [SerializeField]
        private string _statName;
        [SerializeField]
        protected float _baseValue;

        protected StatPreset statPreset;
        public string Name { get => statPreset.Name; }
        [System.NonSerialized]
        protected float value;
        public float Value { get => value; }

        protected StatsHandler statsHandler;

        public virtual void Initialize(StatsHandler handler)
        {
            statsHandler = handler;
            value = _baseValue;
        }
        public virtual void CalculateValue()
        {
            var newValue = _baseValue;
            var modifiers = statsHandler.GetAllModifiers(Name);
            foreach(StatModifier modifier in modifiers)
            {
                    switch(modifier.Type)
                    {
                        case StatModType.Flat :
                            newValue += modifier.Value;
                            break;

                        case StatModType.PercentAdd:
                            var valueToAdd = newValue * modifier.Value;
                            newValue += valueToAdd;
                            break;

                        case StatModType.PercentMult:
                            var multValue = newValue * (modifier.Value);
                            newValue = multValue;
                            break;
                    }           
            }
            value = newValue;
        }
    }
}

