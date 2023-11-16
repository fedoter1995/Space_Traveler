using Assets.Client.GameStructures.Stats.PackedStats;
using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Gear.Weapons;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Stats.Chances;
using SpaceTraveler.GameStructures.Stats.PackedStats;
using SpaceTraveler.GameStructures.Stats.StatModifiers;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    [Serializable]
    public abstract class SpaceCombatStatsHandler : BaseSpaceStatsHandler, IHaveDefenciveStats
    {

        #region Const
        private const string RATE_OF_FIRE = "Rate_Of_Fire";
        private const string PROJECTILE_SPEED = "Projectile_Speed";
        private const string PENETRATIONS_NUMB = "Penetration_Numb";
        #endregion

        [SerializeField, Header("Resist Stats")]
        protected List<Resistance> _resistances;
        [SerializeField, Header("Damages Stats")]
        protected List<Damage> _damages = new List<Damage>();


        [Header("======================================= Dot Stats =======================================")]
        [SerializeField]
        protected List<DotChance> _dotChances = new List<DotChance>();
        [SerializeField]
        protected List<DamageOverTime> _dotDamages = new List<DamageOverTime>();

        [Header("======================================= Lasting Stats =======================================")]
        [SerializeField]
        protected List<Duration> _durations = new List<Duration>();
        [SerializeField]
        protected List<Frequency> _frequencies = new List<Frequency>();


        [Header("======================================= Multipliers Stats =================================")]
        [SerializeField]
        protected List<Multiplier> _multipliers = new List<Multiplier>();
        [SerializeField]
        protected List<MultiplierChance> _multiplieChances = new List<MultiplierChance>();

        protected List<ShootPosition> shotPoints;

        public float ProjectileSpeed { get; private set; }
        public float RateOfFire { get; private set; }
        public int PenetrationsNumb { get; private set; }


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

            base.Initialize(sender);

        }
        public override void CalculateValues(AddedModifiers addedModifiers = null)
        {
            CalculateValuesInList(_stats, addedModifiers);
            CalculateValuesInList(_resistances, addedModifiers);
            CalculateValuesInList(_damages, addedModifiers);

            CalculateValuesInList(_multiplieChances, addedModifiers);
            CalculateValuesInList(_multipliers, addedModifiers);

            CalculateValuesInList(_dotChances, addedModifiers);
            CalculateValuesInList(_dotDamages, addedModifiers);
            CalculateValuesInList(_durations, addedModifiers);
            base.CalculateValues();
        }
        public virtual void SetShootPoints(List<ShootPosition> points)
        {
            shotPoints = points;
        }
        public HitStats GetHitStats(AddedModifiers addedModifiers = null)
        {
            var damageAttributes = new List<DamageAttributes>();
            var damages = new List<Damage>(_damages);


            var packedMultStats = PackMultStats();
            var dotStats = PackDotStats();


            foreach (Damage damage in damages)
            {
                try
                {
                    var dmg = new DamageAttributes((int)damage.Value, damage.Type);
                    damageAttributes.Add(dmg);
                }
                catch
                {
                    throw new Exception($"Cant Add {damage} to {damageAttributes}");
                }
            }

            HitDamage hitDamage = new HitDamage(damageAttributes);

            return new HitStats(mainObject, hitDamage, dotStats, packedMultStats, 0);

        }
        public ShotStats GetShotStats(Vector3 dirrection)
        {
            ShotStats shotStats = new ShotStats(dirrection, shotPoints, ProjectileSpeed);

            return shotStats;
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

            if(addedModifiers != null)
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
            ProjectileSpeed = GetStat(PROJECTILE_SPEED).Value;
            RateOfFire = GetStat(RATE_OF_FIRE).Value;
            PenetrationsNumb = (int)GetStat(PENETRATIONS_NUMB).Value;
            base.OnValuesCalculated();
        }
        private List<PackedDotStats> PackDotStats()
        {
            var damages = new List<DamageOverTime>(_dotDamages);
            var durations = new List<Duration>(_durations);
            var frequencies = new List<Frequency>(_frequencies);
            var dotStats = new List<PackedDotStats>();
            foreach (var chance in _dotChances)
            {
                Duration duration;
                DamageOverTime damage;
                Frequency frequency;

                try
                {
                    damage = damages.Find(dmg => dmg.DamageOverTimeRef == chance.ChancePreset.DamageOverTimeRef);
                    damages.Remove(damage);
                }
                catch (NullReferenceException ex)
                {
                    throw ex;
                }
                try
                {
                    duration = durations.Find(dur => dur.DurationPreset == chance.ChancePreset.DamageOverTimeDurationRef);
                    durations.Remove(duration);
                }
                catch (NullReferenceException ex)
                {
                    throw new ArgumentNullException("Cant find current DotDuration");
                }
                try
                {
                    frequency = frequencies.Find(freq => freq.Preset == chance.ChancePreset.FrequencyRef);
                    frequencies.Remove(frequency);
                }
                catch (NullReferenceException ex)
                {
                    throw new ArgumentNullException("Cant find current DotDuration");
                }

                var damageAttributes = new DamageAttributes((int)damage.Value, damage.DamageType);

                dotStats.Add(new PackedDotStats(damageAttributes, chance.Value, (int)frequency.Value, (int)duration.Value));
            }

            return dotStats;
        }
        private List<PackedMultStats> PackMultStats()
        {
            var multipliers = new List<Multiplier>(_multipliers);
            var packedMultStats = new List<PackedMultStats>();

            foreach (var chance in _multiplieChances)
            {
                Multiplier multiplier;

                try
                {
                    multiplier = multipliers.Find(mult => mult.Preset == chance.MultiplierRef);
                    multipliers.Remove(multiplier);
                }
                catch (NullReferenceException ex)
                {
                    throw new ArgumentNullException("Cant find current DamageOverTime");
                }


                packedMultStats.Add(new PackedMultStats(chance.Value, multiplier.Value, multiplier.DamageType));
            }

            return packedMultStats;
        }
        public List<Resistance> GetResistances()
        {
            return _resistances;
        }

        public List<ActionChance> GetDefenciveActionStats()
        {
            throw new NotImplementedException();
        }
    }
}


