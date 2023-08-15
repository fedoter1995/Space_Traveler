using SpaceTraveler.GameStructures.Stats.StatModifiers;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    [System.Serializable]
    public class Attribute : Stat
    {
        [SerializeField]
        protected List<StatModifier> _statModifiers = new List<StatModifier>();
        public List<StatModifier> Modifiers => _statModifiers;

        public override void CalculateValue()
        {
            var newValue = _baseValue;
            var modifiers = statsHandler.GetAllModifiers();
            foreach (StatModifier modifier in modifiers)
            {
                if (modifier.HasInfluenceToStat(Name))
                {

                    if (modifier.Type == StatModType.Flat)
                    {
                        newValue += modifier.Value;
                    }
                    if (modifier.Type == StatModType.PercentAdd)
                    {
                        var valueToAdd = newValue * modifier.Value;
                        newValue += valueToAdd;
                    }
                    if (modifier.Type == StatModType.PercentMult)
                    {
                        var multValue = newValue * (modifier.Value + 1);
                        newValue = multValue;
                    }
                }
            }
            value = newValue;
        }
    }
}

