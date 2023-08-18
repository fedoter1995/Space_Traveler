using SpaceTraveler.GameStructures.Gear.Weapons;
using SpaceTraveler.GameStructures.Stats.Scripts;
using SpaceTraveler.GameStructures.Stats.StatModifiers;
using System;
using System.Collections.Generic;
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
        protected List<Damage> _damages;
        [SerializeField, Header("Chances Stats")]
        protected List<Chance> _chances;
        [SerializeField, Header("Multipliers Stats")]
        protected List<Multiplier> _multipliers;

        protected List<ShootPosition> shotPoints;

        public float ProjectileSpeed { get; private set; }
        public float RateOfFire { get; private set; }
        public int PenetrationsNumb { get; private set; }


        public override event Action OnCalculateValuesEvent;
        
        public override void Initialize(object sender)
        {      
            InitializeStats(_resistances);
            InitializeStats(_damages);
            InitializeStats(_chances);
            InitializeStats(_multipliers);
            base.Initialize(sender);

        }
        public override void CalculateValues()
        {
            CalculateValuesInList(_resistances);
            CalculateValuesInList(_damages);
            CalculateValuesInList(_chances);
            CalculateValuesInList(_multipliers);
            base.CalculateValues();
        }
        public virtual void SetShootPoints(List<ShootPosition> points)
        {
            shotPoints = points;
        }
        public HitStats GetHitStats()
        {
            var DamageTypeValue = new List<DamageAttributes>();
            foreach (Damage damage in _damages)
            {
                try
                {
                    var dmg = new DamageAttributes((int)damage.Value, damage.Type);
                    DamageTypeValue.Add(dmg);
                }
                catch
                {
                    throw new Exception($"Cant Add {damage} to {DamageTypeValue}");
                }
            }

            HitDamage shotDamage = new HitDamage(DamageTypeValue);

            return new HitStats(mainObject, shotDamage, _chances, _multipliers, (int)PenetrationsNumb);
        }
        public ShotStats GetShotStats(Vector3 dirrection)
        {
            Debug.Log(ProjectileSpeed);
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
            ProjectileSpeed = GetStat(PROJECTILE_SPEED).Value;
            RateOfFire = GetStat(RATE_OF_FIRE).Value;
            PenetrationsNumb = (int)GetStat(PENETRATIONS_NUMB).Value;
            base.OnValuesCalculated();
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


