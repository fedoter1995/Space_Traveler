using SpaceTraveler.GameStructures.Stats;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Tests
{
    [Serializable]
    public class TestResistanceseStatsHandler : StatsHandler
    {
        [SerializeField, Header("Stats")]
        private List<Resistance> _stats;
        [SerializeField, Header("Resistance")]
        private List<Resistance> _resistances;

        public List<Resistance> Resistances => _resistances;
        public override void CalculateValues()
        {
            CalculateValuesInList(_stats);
            CalculateValuesInList(_resistances);
            OnValuesCalculated();
        }
        public override BaseStat GetStat(string statName)
        {
            var stats = new List<BaseStat>(_stats);
            stats.AddRange(_resistances);

            return stats.Find(stat => stat.Name == statName);
        }

        public override void Initialize(object sender)
        {
            InitializeStats(_stats);
            InitializeStats(_resistances);
            CalculateValues();
        }

        protected override void OnValuesCalculated()
        {
            
        }
    }
}

