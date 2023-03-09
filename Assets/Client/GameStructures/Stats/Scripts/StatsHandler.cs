using Architecture;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Stats
{
    [Serializable]
    public abstract class StatsHandler
    {

        /// A set of basic stats
        [SerializeField]
        protected EnvironmentSettings _environment;

        [SerializeField,Header("Standart Stats")]
        protected List<Stat> _stats;

        public bool IsInitialize { get; private set; } = false;
        public EnvironmentSettings CurrentEnvironment => _environment;

        protected abstract void OnValuesCalculated();
        public virtual void Initialize()
        {
            InitializeStats(_stats);
            CalculateValues();
            IsInitialize = true;
            OnValuesCalculated();
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
        public virtual void CalculateValues()
        {
            if(_environment == null)
            {
                _environment = Game.DefaultEnvironment;
            }
            CalculateValuesInList(_stats);
            OnValuesCalculated();
        }
        public List<Stat> GetStats()
        {
            var newList = new List<Stat>(_stats);

            return newList;
        }
        public Stat GetStat(string statName)
        {
            var findedStat = _stats.Find(stat => stat.Name == statName);
            return findedStat;
        }
        public void SetEnvironment(EnvironmentSettings environment)
        {
            _environment = environment;
            CalculateValues();
        }  
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
            for (int i = 0; i < stats.Count; i++)
            {
                stats[i].Initialize(this);
            }
        }
        protected void CalculateValuesInList<T>(List<T> stats) where T : BaseStat
        {
            for (int i = 0; i < stats.Count; i++)
            {
                stats[i].CalculateValue();
            }
        }

    }
}

