using Architecture;
using SpaceTraveler.GameStructures.Gear.Weapons;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Player;
using SpaceTraveler.GameStructures.Stats.StatModifiers;
using SpaceTraveler.GameStructures.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Enemy.HumanoidEnemyes
{
    [CreateAssetMenu(menuName = "StatsHandler/MeleHumanoidEnemyStatsHandler")]
    public class MeleHumanoidEnemyStatsHandler : StatsHandler, IHaveResistances
    {
        private const string HEALTH_POINTS = "Max_Health_Points";
        private const string MOVE_SPEED = "Max_Movement_Speed";

        [SerializeField, Header("Standart Stats")]
        protected List<Stat> _stats;
        [SerializeField, Header("Resist Stats")]
        protected List<Resistance> _resistances;
        [SerializeField, Header("Damages Stats")]
        protected List<Damage> _damages;
        [SerializeField, Header("Chances Stats")]
        protected List<Chance> _chances;
        [SerializeField, Header("Multipliers Stats")]
        protected List<Multiplier> _multipliers;


        private HumanoidEnemy sender;


        public float HealthPoints { get; private set; }
        public float MoveSpeed { get; private set; } = 2f;

        public override event Action OnCalculateValuesEvent;


        public override void Initialize()
        {
            InitializeStats(_resistances);
            InitializeStats(_damages);
            InitializeStats(_chances);
            InitializeStats(_multipliers);
            InitializeStats(_stats);
            base.Initialize();
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
            CalculateValuesInList(_chances);
            CalculateValuesInList(_multipliers);

            OnValuesCalculated();
        }

        public HitStats GetHitStats()
        {
            var DamageTypeValues = new List<DamageTypeValue>();
            foreach (Damage damage in _damages)
            {
                try
                {
                    var dmg = new DamageTypeValue((int)damage.Value, damage.Type);
                    DamageTypeValues.Add(dmg);
                }
                catch
                {
                    throw new Exception($"Cant Add {damage} to {DamageTypeValues}");
                }
            }

            HitDamage hitDamage = new HitDamage(DamageTypeValues);

            return new HitStats(hitDamage, _chances, _multipliers, 0);
        }
        public HitStats GetHitStats(AddedModifiers addedModifiers = null)
        {
            var DamageTypeValues = new List<DamageTypeValue>();
            var damages = new List<Damage>(_damages);
            var chances = new List<Chance>(_chances);
            var multipliers = new List<Multiplier>(_multipliers);

            if (addedModifiers != null)
            {
                damages.AddRange(addedModifiers.AddedDamages);
                chances.AddRange(addedModifiers.AddedChances);
                multipliers.AddRange(addedModifiers.AddedMultipliers);
            }


            foreach (Damage damage in damages)
            {
                try
                {
                    var dmg = new DamageTypeValue((int)damage.Value, damage.Type);
                    DamageTypeValues.Add(dmg);
                }
                catch
                {
                    throw new Exception($"Cant Add {damage} to {DamageTypeValues}");
                }
            }

            HitDamage hitDamage = new HitDamage(DamageTypeValues);

            return new HitStats(hitDamage, chances, multipliers, 0);

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
            allStats.AddRange(_chances);
            allStats.AddRange(_multipliers);

            return allStats.Find(stat => stat.Name == statName);
        }
        protected override void OnValuesCalculated()
        {
            HealthPoints = GetStat(HEALTH_POINTS).Value;
            MoveSpeed = GetStat(MOVE_SPEED).Value;

            OnCalculateValuesEvent?.Invoke();
        }

        public List<Resistance> GetResistances()
        {
            CalculateValues();
            return _resistances;
        }
    }
}
