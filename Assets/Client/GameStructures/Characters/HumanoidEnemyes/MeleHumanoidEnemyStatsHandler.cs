using Architecture;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Stats.StatModifiers;
using SpaceTraveler.GameStructures.Stats;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using SpaceTraveler.GameStructures.Stats.Chances;
using Assets.Client.GameStructures.Stats.PackedStats;
using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Stats.PackedStats;

namespace SpaceTraveler.GameStructures.Characters.HumanoidEnemyes
{
    [CreateAssetMenu(menuName = "StatsHandler/MeleHumanoidEnemyStatsHandler")]
    public class MeleHumanoidEnemyStatsHandler : CombatStatsHandler, IHaveDefenciveStats
    {
        private const string HEALTH_POINTS = "Max_Health_Points";
        private const string MOVE_SPEED = "Max_Movement_Speed";

        [SerializeField, Header("Standart Stats")]
        protected List<Stat> _stats = new List<Stat>();
        [SerializeField, Header("Resist Stats")]
        protected List<Resistance> _resistances = new List<Resistance>();



        public float MaxHealthPoints { get; private set; }
        public float MoveSpeed { get; private set; } = 2f;

        public override event Action OnCalculateValuesEvent;

        public override void Initialize(object sender)
        {
            InitializeStats(_resistances);
            InitializeStats(_damages);
            InitializeStats(_multiplieChances);
            InitializeStats(_multipliers);
            InitializeStats(_dotChances);
            InitializeStats(_dotDamages);
            InitializeStats(_durations);

            InitializeStats(_stats);
            base.Initialize(sender);
        }

        public override void CalculateValues(AddedModifiers addedModifiers = null)
        {
            if (_environment == null)
            {
                _environment = Game.DefaultEnvironment;
            }
            CalculateValuesInList(_stats, addedModifiers);
            CalculateValuesInList(_resistances, addedModifiers);
            CalculateValuesInList(_damages, addedModifiers);

            CalculateValuesInList(_multiplieChances, addedModifiers);
            CalculateValuesInList(_multipliers, addedModifiers);

            CalculateValuesInList(_dotChances, addedModifiers);
            CalculateValuesInList(_dotDamages, addedModifiers);
            CalculateValuesInList(_durations, addedModifiers);

            OnValuesCalculated();
        }

        public override List<StatModifier> GetAllModifiers()
        {
            var modifierList = new List<StatModifier>();

            modifierList.AddRange(CurrentEnvironment.Modifiers);

            var arrangeList = new List<StatModifier>(ArrangeModifiers(modifierList));

            return arrangeList;
        }
        public override List<StatModifier> GetAllModifiers(string targetStatName, AddedModifiers addedModifiers = null)
        {
            var modifierList = new List<StatModifier>();
            var relevantModifiers = new List<StatModifier>();
            modifierList.AddRange(CurrentEnvironment.Modifiers);
            if (addedModifiers != null)
                modifierList.AddRange(addedModifiers.Modifiers);

            relevantModifiers = modifierList.FindAll(modifier => modifier.HasInfluenceToStat(targetStatName));

            var arrangeList = new List<StatModifier>(ArrangeModifiers(relevantModifiers));
            return arrangeList;
        }
        public override BaseStat GetStat(string statName)
        {
            var allStats = new List<BaseStat>(_stats);
            allStats.AddRange(_damages);
            allStats.AddRange(_resistances);
            allStats.AddRange(_multiplieChances);
            allStats.AddRange(_multipliers);
            allStats.AddRange(_dotChances);
            allStats.AddRange(_dotDamages);
            allStats.AddRange(_durations);

            return allStats.Find(stat => stat.Name == statName);
        }
        protected override void OnValuesCalculated()
        {
            MaxHealthPoints = GetStat(HEALTH_POINTS).Value;
            MoveSpeed = GetStat(MOVE_SPEED).Value;

            OnCalculateValuesEvent?.Invoke();
        }

        public List<Resistance> GetResistances()
        {
            CalculateValues();
            return _resistances;
        }

        public List<ActionChance> GetDefenciveActionStats()
        {
            throw new NotImplementedException();
        }
    }
}
