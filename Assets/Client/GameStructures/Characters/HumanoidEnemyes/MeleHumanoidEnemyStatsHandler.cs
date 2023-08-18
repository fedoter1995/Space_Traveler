﻿using Architecture;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Stats.StatModifiers;
using SpaceTraveler.GameStructures.Stats;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using SpaceTraveler.GameStructures.Stats.Chances;

namespace SpaceTraveler.GameStructures.Characters.HumanoidEnemyes
{
    [CreateAssetMenu(menuName = "StatsHandler/MeleHumanoidEnemyStatsHandler")]
    public class MeleHumanoidEnemyStatsHandler : StatsHandler, IHaveDefenciveStats
    {
        private const string HEALTH_POINTS = "Max_Health_Points";
        private const string MOVE_SPEED = "Max_Movement_Speed";

        [SerializeField, Header("Standart Stats")]
        protected List<Stat> _stats = new List<Stat>();
        [SerializeField, Header("Resist Stats")]
        protected List<Resistance> _resistances = new List<Resistance>();
        [SerializeField, Header("Damages Stats")]
        protected List<Damage> _damages = new List<Damage>();
        [SerializeField, Header("Chances Stats")]
        protected List<MultiplierChance> _multiplierChances = new List<MultiplierChance>();
        [SerializeField, Header("Multipliers Stats")]
        protected List<Multiplier> _multipliers = new List<Multiplier>();


        public float MaxHealthPoints { get; private set; }
        public float MoveSpeed { get; private set; } = 2f;

        public override event Action OnCalculateValuesEvent;

        public override void Initialize(object sender)
        {
            InitializeStats(_resistances);
            InitializeStats(_damages);
            InitializeStats(_multiplierChances);
            InitializeStats(_multipliers);
            InitializeStats(_stats);
            base.Initialize(sender);
        }

        public override void CalculateValues()
        {
            if (_environment == null)
            {
                _environment = Game.DefaultEnvironment;
            }
            CalculateValuesInList(_stats);
            CalculateValuesInList(_resistances);
            CalculateValuesInList(_damages);
            CalculateValuesInList(_multiplierChances);
            CalculateValuesInList(_multipliers);

            OnValuesCalculated();
        }

        public HitStats GetHitStats()
        {
            var chances = new List<Chance>(_multiplierChances);
            var DamageAttributes = new List<DamageAttributes>();
            foreach (Damage damage in _damages)
            {
                try
                {
                    var dmg = new DamageAttributes((int)damage.Value, damage.Type);
                    DamageAttributes.Add(dmg);
                }
                catch
                {
                    throw new Exception($"Cant Add {damage} to {DamageAttributes}");
                }
            }

            HitDamage hitDamage = new HitDamage(DamageAttributes);

            return new HitStats(mainObject, hitDamage, chances, _multipliers, 0);
        }
        public HitStats GetHitStats(AddedModifiers addedModifiers = null)
        {
            var DamageTypeValues = new List<DamageAttributes>();
            var damages = new List<Damage>(_damages);
            var chances = new List<Chance>(_multiplierChances);
            var multipliers = new List<Multiplier>(_multipliers);

            if (addedModifiers != null)
            {
                
            }


            foreach (Damage damage in damages)
            {
                try
                {
                    var dmg = new DamageAttributes((int)damage.Value, damage.Type);
                    DamageTypeValues.Add(dmg);
                }
                catch
                {
                    throw new Exception($"Cant Add {damage} to {DamageTypeValues}");
                }
            }

            HitDamage hitDamage = new HitDamage(DamageTypeValues);

            return new HitStats(mainObject, hitDamage, chances, multipliers, 0);

        }
        public override List<StatModifier> GetAllModifiers()
        {
            var modifierList = new List<StatModifier>();

            modifierList.AddRange(CurrentEnvironment.Modifiers);

            var arrangeList = new List<StatModifier>(ArrangeModifiers(modifierList));

            return arrangeList;
        }
        public override List<StatModifier> GetAllModifiers(string targetStatName)
        {
            var modifierList = new List<StatModifier>();
            var relevantModifiers = new List<StatModifier>();
            modifierList.AddRange(CurrentEnvironment.Modifiers);
            relevantModifiers = modifierList.FindAll(modifier => modifier.HasInfluenceToStat(targetStatName));

            var arrangeList = new List<StatModifier>(ArrangeModifiers(relevantModifiers));
            return arrangeList;
        }
        public override BaseStat GetStat(string statName)
        {
            var allStats = new List<BaseStat>(_stats);
            allStats.AddRange(_damages);
            allStats.AddRange(_resistances);
            allStats.AddRange(_multiplierChances);
            allStats.AddRange(_multipliers);

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