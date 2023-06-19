using System;
using System.Collections.Generic;
using UnityEngine;
using Architecture;
using Newtonsoft.Json.Linq;
using GameStructures.Stats;
using GameStructures.Gear.Weapons;
using GameStructures.Gear;
using GameStructures.Hits;
using GameStructures.Spaceship;

[Serializable]
[CreateAssetMenu(menuName = "StatsHandler/Ship_Stats")]
public class StarshipStatsHandler : StatsHandler, IJsonSerializable
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
    [SerializeField,Header("Damages")]
    private List<Damage> _damages;
    [SerializeField, Header("Resistances")]
    private List<Resistance> _resistances;
    [SerializeField, Header("Chances")]
    private List<Chance> _chances;
    [SerializeField, Header("Multipliers")]
    private List<Multiplier> _multipliers;

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


    public EquipmentHandler Equipment { get; private set; }
    public Starship Spaceship { get; private set; }
    public List<Resistance> Resistances => _resistances;


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
        var DamageTypeValue = new List<DamageTypeValue>();
        foreach (Damage damage in _damages)
        {
            try
            {
                var dmg = new DamageTypeValue((int)damage.Value, damage.Type);
                DamageTypeValue.Add(dmg);
            }
            catch
            {
                throw new Exception($"Cant Add {damage} to {DamageTypeValue}");
            }
        }

        HitDamage shotDamage = new HitDamage(DamageTypeValue);               

        return new HitStats(shotDamage, _chances, _multipliers, (int)PenetrationsNumb);
    }
    public ShotStats GetShotStats()
    {
        ShotStats shotStats = new ShotStats(Spaceship.transform.up, shotPoints, ProjectileSpeed);

        return shotStats;
    }
    public override List<StatModifier> GetAllModifiers(string targetStatName)
    {
        var modifierList = new List<StatModifier>();
        var relevantModifiers = new List<StatModifier>();

        //modifierList.AddRange(AttributesMultModifiers());
        modifierList.AddRange(Equipment.GetAllModifiers());
        modifierList.AddRange(CurrentEnvironment.Modifiers);

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
    public override void Initialize()
    {
        InitializeStats(_stats);
        InitializeStats(_resistances);
        InitializeStats(_damages);
        InitializeStats(_chances);
        InitializeStats(_multipliers);
        Spaceship = Game.GetInteractor<SpaceshipInteractor>().spaceship;
        Equipment = Spaceship.Equipment;
        Equipment.OnEquipmentChangeEvent += CalculateValues;
        CalculateValues();
        shotPoints = new List<ShootPosition>(Spaceship.gameObject.GetComponentsInChildren<ShootPosition>());
    }

    public  void SetObjectData(Dictionary<string, object> data)
    {
        if(data != null)
        {
            _stats = new List<Stat>();
            _damages = new List<Damage>();
            _resistances = new List<Resistance>();
            _chances = new List<Chance>();
            _multipliers = new List<Multiplier>();

            JObject statsData = (JObject)data["Stats_Data"];
            JObject damageData = (JObject)data["Damages_Data"];
            JObject resistanceData = (JObject)data["Resistances_Data"];
            JObject chancesData = (JObject)data["Chances_Data"];
            JObject multipliersData = (JObject)data["Multipliers_Data"];

            var stats = statsData.ToObject<Dictionary<string, Dictionary<string, object>>>();
            var damages = damageData.ToObject<Dictionary<string, Dictionary<string, object>>>();
            var resistances = resistanceData.ToObject<Dictionary<string, Dictionary<string, object>>>();
            var chances = chancesData.ToObject<Dictionary<string, Dictionary<string, object>>>();
            var multipliers = multipliersData.ToObject<Dictionary<string, Dictionary<string, object>>>();

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
                Chance newChance = new Chance();
                newChance.SetObjectData(entry.Value);
                _chances.Add(newChance);
            }
            foreach (KeyValuePair<string, Dictionary<string, object>> entry in multipliers)
            {
                Multiplier newMultiplier = new Multiplier();
                newMultiplier.SetObjectData(entry.Value);
                _multipliers.Add(newMultiplier);
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
        foreach (Stat stat in _stats)
        {
            statsData.Add(stat.Name, stat.GetObjectData());
        }
        foreach (Damage damage in _damages)
        {
            damageData.Add(damage.Name, damage.GetObjectData());
        }
        foreach (Resistance resistance in _resistances)
        {
            resistanceData.Add(resistance.Name, resistance.GetObjectData());
        }
        foreach (Chance chance in _chances)
        {
            chancesData.Add(chance.Name, chance.GetObjectData());
        }
        foreach (Multiplier multiplier in _multipliers)
        {
            multipliersData.Add(multiplier.Name, multiplier.GetObjectData());
        }

        data.Add("Stats_Data", statsData);
        data.Add("Damages_Data", damageData);
        data.Add("Resistances_Data", resistanceData);
        data.Add("Chances_Data", chancesData);
        data.Add("Multipliers_Data", multipliersData);

        return data;
    }

    public override BaseStat GetStat(string statName)
    {
        var stats = new List<BaseStat>(_stats);

        stats.AddRange(_damages);
        stats.AddRange(_resistances);
        stats.AddRange(_chances);
        stats.AddRange(_multipliers);

        
        return FindStatInList(stats, statName);

    }
    private float GetStatValue(string statName)
    {
        var stat = GetStat(statName);

        if (stat == null)
            return 0;

        return stat.Value;

    }

}

