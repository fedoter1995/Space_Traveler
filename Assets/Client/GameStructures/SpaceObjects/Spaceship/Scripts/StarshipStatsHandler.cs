using System;
using System.Collections.Generic;
using UnityEngine;
using Architecture;
using Newtonsoft.Json.Linq;
using SpaceTraveler.GameStructures.Gear.Weapons;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Gear.Spaceship;
using SpaceTraveler.GameStructures.Stats.StatModifiers;
using SpaceTraveler.GameStructures.Stats.Chances;
using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Hits;

namespace SpaceTraveler.GameStructures.Spaceship
{
    [Serializable]
    [CreateAssetMenu(menuName = "StatsHandler/Ship_Stats")]
    public class StarshipStatsHandler : CombatStatsHandler, IJsonSerializable, IHaveDefenciveStats
    {
        #region Const
        private const string HEALTH_POINTS = "Max_Health_Points";
        private const string MOVE_SPEED = "Max_Movement_Speed";
        private const string ACCELERATION = "Acceleration";
        private const string DECELERATION = "Deceleration";
        private const string SWING_SPEED = "Swing_Speed";
        private const string SWING_SPEEDUP = "Swing_Speedup";
        private const string SWING_SLOWDOWN = "Swing_Slowdown";
        private const string RATE_OF_FIRE = "Rate_Of_Fire";
        private const string SHOT_SPEED = "Projectile_Speed";
        #endregion

        [SerializeField, Header("Stats")]
        private List<Stat> _stats;
        [SerializeField, Header("Resistances")]
        private List<Resistance> _resistances;


        private List<ShootPosition> shotPoints;

        public float HealthPoints { get; private set; }
        public float MoveSpeed { get; private set; }
        public float Acceleration { get; private set; }
        public float Deceleration { get; private set; }
        public float SwingSpeed { get; private set; }
        public float SwingSpeedup { get; private set; }
        public float SwingSlowdown { get; private set; }
        public float ProjectileSpeed { get; private set; }
        public float RateOfFire { get; private set; }
        public int PenetrationsNumb { get; private set; }


        public SpaceshipModuleHandler Equipment { get; private set; }
        public Starship Spaceship { get; private set; }

        public override void Initialize(object sender)
        {
            InitializeStats(_resistances);
            InitializeStats(_damages);
            InitializeStats(_multiplieChances);
            InitializeStats(_multipliers);
            InitializeStats(_dotChances);
            InitializeStats(_dotDamages);
            InitializeStats(_durations);
            InitializeStats(_frequencies);

            Spaceship = Game.GetInteractor<SpaceshipInteractor>().spaceship;
            Equipment = Spaceship.Equipment as SpaceshipModuleHandler;
            Equipment.OnEquipmentChangeEvent += CalculateValues;
            shotPoints = new List<ShootPosition>(Spaceship.gameObject.GetComponentsInChildren<ShootPosition>());

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
            CalculateValuesInList(_frequencies, addedModifiers);

            OnValuesCalculated();
        }
        private void CalculateValues()
        {
            CalculateValues();
        }
        public ShotStats GetShotStats()
        {
            ShotStats shotStats = new ShotStats(Spaceship.transform.up, shotPoints, ProjectileSpeed);

            return shotStats;
        }
        public override List<StatModifier> GetAllModifiers(string targetStatName, AddedModifiers addedModifiers = null)
        {
            var modifierList = new List<StatModifier>();
            var relevantModifiers = new List<StatModifier>();

            //modifierList.AddRange(AttributesMultModifiers());
            modifierList.AddRange(Equipment.GetAllModifiers());
            modifierList.AddRange(CurrentEnvironment.Modifiers);
            if (addedModifiers != null)
                modifierList.AddRange(addedModifiers.Modifiers);


            relevantModifiers = modifierList.FindAll(modifier => modifier.HasInfluenceToStat(targetStatName));

            var arrangeList = new List<StatModifier>(ArrangeModifiers(relevantModifiers));
            return arrangeList;
        }
        public override List<StatModifier> GetAllModifiers()
        {
            var modifierList = new List<StatModifier>();
            //modifierList.AddRange(AttributesMultModifiers());
            modifierList.AddRange(Equipment.GetAllModifiers());
            modifierList.AddRange(CurrentEnvironment.Modifiers);
            var arrangeList = new List<StatModifier>(ArrangeModifiers(modifierList));

            return arrangeList;
        }
        public List<Resistance> GetResistances()
        {
            CalculateValues();

            return _resistances;
        }
        public  void SetObjectData(Dictionary<string, object> data)
        {
            if(data != null)
            {
                _stats = new List<Stat>();
                _damages = new List<Damage>();
                _resistances = new List<Resistance>();
                _multiplieChances = new List<MultiplierChance>();
                _multipliers = new List<Multiplier>();
                _dotChances = new List<DotChance>();
                _dotDamages = new List<DamageOverTime>();
                _durations = new List<Duration>();

                JObject statsData = (JObject)data["Stats_Data"];
                JObject damageData = (JObject)data["Damages_Data"];
                JObject resistanceData = (JObject)data["Resistances_Data"];
                JObject chancesData = (JObject)data["Chances_Data"];
                JObject multipliersData = (JObject)data["Multipliers_Data"];
                JObject dotChancesData = (JObject)data["DotChances_Data"];
                JObject dotDamageData = (JObject)data["DotDamages_Data"];
                JObject dotDurationData = (JObject)data["DotDurations_Data"];



                var stats = statsData.ToObject<Dictionary<string, Dictionary<string, object>>>();
                var damages = damageData.ToObject<Dictionary<string, Dictionary<string, object>>>();
                var resistances = resistanceData.ToObject<Dictionary<string, Dictionary<string, object>>>();
                var chances = chancesData.ToObject<Dictionary<string, Dictionary<string, object>>>();
                var multipliers = multipliersData.ToObject<Dictionary<string, Dictionary<string, object>>>();
                var dotChances = dotChancesData.ToObject<Dictionary<string, Dictionary<string, object>>>();
                var dotDamages = dotDamageData.ToObject<Dictionary<string, Dictionary<string, object>>>();
                var dotDurations = dotDurationData.ToObject<Dictionary<string, Dictionary<string, object>>>();


                foreach (KeyValuePair<string, Dictionary<string, object>> entry in stats)
                {
                    Stat newStat = new Stat();
                    newStat.SetObjectData(entry.Value);
                    _stats.Add(newStat);
                }
                foreach (KeyValuePair<string, Dictionary<string, object>> entry in damages)
                {
                    Damage newDamage = new Damage();
                    newDamage.SetObjectData(entry.Value);
                    _damages.Add(newDamage);
                }
                foreach (KeyValuePair<string, Dictionary<string, object>> entry in resistances)
                {
                    Resistance newResistance = new Resistance();
                    newResistance.SetObjectData(entry.Value);
                    _resistances.Add(newResistance);
                }
                foreach (KeyValuePair<string, Dictionary<string, object>> entry in chances)
                {
                    MultiplierChance newChance = new MultiplierChance();
                    newChance.SetObjectData(entry.Value);
                    _multiplieChances.Add(newChance);
                }
                foreach (KeyValuePair<string, Dictionary<string, object>> entry in multipliers)
                {
                    Multiplier newMultiplier = new Multiplier();
                    newMultiplier.SetObjectData(entry.Value);
                    _multipliers.Add(newMultiplier);
                }
                foreach (KeyValuePair<string, Dictionary<string, object>> entry in dotChances)
                {
                    DotChance newDotChance= new DotChance();
                    newDotChance.SetObjectData(entry.Value);
                    _dotChances.Add(newDotChance);
                }
                foreach (KeyValuePair<string, Dictionary<string, object>> entry in dotDamages)
                {
                    DamageOverTime newDot = new DamageOverTime();
                    newDot.SetObjectData(entry.Value);
                    _dotDamages.Add(newDot);
                }
                foreach (KeyValuePair<string, Dictionary<string, object>> entry in dotDurations)
                {
                    Duration newduration = new Duration();
                    newduration.SetObjectData(entry.Value);
                    _durations.Add(newduration);
                }
            }
        }
        public  Dictionary<string, object> GetObjectData()
        {        
            var data = new Dictionary<string, object>();

            //var atributesData = new Dictionary<string, object>();
            var statsData = new Dictionary<string, object>();
            var damageData = new Dictionary<string, object>();
            var resistanceData = new Dictionary<string, object>();
            var chancesData = new Dictionary<string, object>();
            var multipliersData = new Dictionary<string, object>();
            var dotChanceData = new Dictionary<string, object>();
            var dotDamageData = new Dictionary<string, object>();
            var dotDurationData = new Dictionary<string, object>();


            foreach (Stat stat in _stats)           
                statsData.Add(stat.Name, stat.GetObjectData());
            
            foreach (Damage damage in _damages)           
                damageData.Add(damage.Name, damage.GetObjectData());
            
            foreach (Resistance resistance in _resistances)          
                resistanceData.Add(resistance.Name, resistance.GetObjectData());
            
            foreach (MultiplierChance chance in _multiplieChances)          
                chancesData.Add(chance.Name, chance.GetObjectData());
            
            foreach (Multiplier multiplier in _multipliers)           
                multipliersData.Add(multiplier.Name, multiplier.GetObjectData());
            
            foreach (DotChance chance in _dotChances)
                dotChanceData.Add(chance.Name, chance.GetObjectData());
            
            foreach (DamageOverTime damage in _dotDamages)
                dotDamageData.Add(damage.Name, damage.GetObjectData());
            
            foreach (Duration duration in _durations)
                dotDurationData.Add(duration.Name, duration.GetObjectData());
            

            data.Add("Stats_Data", statsData);
            data.Add("Damages_Data", damageData);
            data.Add("Resistances_Data", resistanceData);
            data.Add("MultiplieChances_Data", chancesData);
            data.Add("Multipliers_Data", multipliersData);
            data.Add("DotChances_Data", dotChanceData);
            data.Add("DotDamages_Data", dotDamageData);
            data.Add("DotDurations_Data", dotDurationData);

            return data;
        }
        public override BaseStat GetStat(string statName)
        {
            var stats = new List<BaseStat>(_stats);

            stats.AddRange(_damages);
            stats.AddRange(_resistances);
            stats.AddRange(_multiplieChances);
            stats.AddRange(_multipliers);
            stats.AddRange(_dotChances);
            stats.AddRange(_dotDamages);
            stats.AddRange(_durations);

        
            return FindStatInList(stats, statName);

        }
        protected override void OnValuesCalculated()
        {
            HealthPoints = GetStatValue(HEALTH_POINTS);
            MoveSpeed = GetStatValue(MOVE_SPEED);
            SwingSpeed = GetStatValue(SWING_SPEED);
            Acceleration = GetStatValue(ACCELERATION);
            Deceleration = GetStatValue(DECELERATION);
            SwingSpeedup = GetStatValue(SWING_SPEEDUP);
            SwingSlowdown = GetStatValue(SWING_SLOWDOWN);
            RateOfFire = GetStatValue(RATE_OF_FIRE);
            ProjectileSpeed = GetStatValue(SHOT_SPEED);

        }
        private float GetStatValue(string statName)
        {
            var stat = GetStat(statName);

            if (stat == null)
                return 0;

            return stat.Value;

        }

        public List<ActionChance> GetDefenciveActionStats()
        {
            throw new NotImplementedException();
        }
    }
}


