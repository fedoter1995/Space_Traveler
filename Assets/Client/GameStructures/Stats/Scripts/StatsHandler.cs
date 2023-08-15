using SpaceTraveler.GameStructures.Stats.StatModifiers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    [Serializable]
    public abstract class StatsHandler : ScriptableObject
    {
        /// A
        [SerializeField]
        protected StatsHandlerType _handlerType;
        /// A set of basic stats
        [SerializeField]
        protected EnvironmentSettings _environment;

        public virtual event Action OnCalculateValuesEvent;

        public bool IsInitialize { get; private set; } = false;
        public EnvironmentSettings CurrentEnvironment => _environment;
        
        public void SetEnvironment(EnvironmentSettings environment)
        {
            _environment = environment;
            CalculateValues();
        }  
        public abstract void CalculateValues();
        public abstract BaseStat GetStat(string statName);
        public virtual void Initialize()
        {
            CalculateValues();
            IsInitialize = true;
        }
        public virtual List<StatModifier> GetAllModifiers()
        {
            var modifierList = new List<StatModifier>();

            modifierList.AddRange(CurrentEnvironment.Modifiers);

            var arrangeList = new List<StatModifier>(ArrangeModifiers(modifierList));

            return arrangeList;
        }
        public virtual List<StatModifier> GetAllModifiers(string targetStatName)
        {
            var modifierList = new List<StatModifier>();
            var relevantModifiers = new List<StatModifier>();
            modifierList.AddRange(CurrentEnvironment.Modifiers);

            relevantModifiers = modifierList.FindAll(modifier => modifier.HasInfluenceToStat(targetStatName));

            var arrangeList = new List<StatModifier>(ArrangeModifiers(relevantModifiers));
            return arrangeList;
        }

        protected abstract void OnValuesCalculated();
        protected List<StatModifier> ArrangeModifiers(List<StatModifier> sourceList)
        {
            var flatModifiers = new List<StatModifier>();
            var percentAddModifiers = new List<StatModifier>();
            var multModifiers = new List<StatModifier>();

            foreach (StatModifier modifier in sourceList)
            {
                switch(modifier.Type)
                {
                    case StatModType.Flat:
                        flatModifiers.Add(modifier);
                        break;
                    case StatModType.PercentMult:
                        multModifiers.Add(modifier);
                        break;
                    case StatModType.PercentAdd:
                        percentAddModifiers.Add(modifier);
                        break;
                }
            }
            var modifiers = new List<StatModifier>(flatModifiers);

            modifiers.AddRange(multModifiers);
            modifiers.AddRange(percentAddModifiers);

            return modifiers;
        }
        protected void InitializeStats<T>(List<T> stats) where T : BaseStat
        {
            if(stats != null)
                for (int i = 0; i < stats.Count; i++)
                {
                    stats[i].Initialize(this);
                }
        }
        protected void CalculateValuesInList<T>(List<T> stats) where T : BaseStat
        {
            if(stats != null)
            { 
                for (int i = 0; i < stats.Count; i++)
                {
                    stats[i].CalculateValue();
                }
            }

        }
        protected T FindStatInList<T>(List<T> stats, string name) where T : BaseStat
        {
            if(stats != null)
                foreach(T stat in stats)
                {
                    if (stat.Name == name)
                        return stat;
                }
            return null;
        }

    }

    public enum StatsHandlerType
    {
        Player,
        Spaceship,
        Enemy,
        Boss,
        Asteroid
    }
}

