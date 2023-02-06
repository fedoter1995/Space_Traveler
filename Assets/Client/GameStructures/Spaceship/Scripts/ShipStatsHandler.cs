using System;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using CustomTools;

public class ShipStatsHandler : StatsHandler
{
    #region Const
    private const string HEALTHPOINTS = "Max_Health_Points";
    private const string MOVE_SPEED = "Max_Movement_Speed";
    private const string ACCELERATION = "Acceleration";
    private const string DECELERATION = "Deceleration";
    private const string SWING_SPEED = "Swing_Speed";
    private const string SWING_SPEEDUP = "Swing_Speedup";
    private const string SWING_SLOWDOWN = "Swing_Slowdown";
    private const string RATE_OF_FIRE = "Rate_Of_Fire";
    private const string SHOT_SPEED = "Projectile_Speed";
    #endregion
    [SerializeField,Header("Damages")]
    private List<Damage> _damages;
    [SerializeField, Header("Attributes")]
    private List<Stats.Attribute> _attributes;
    [SerializeField, Header("Resistances")]
    private List<Resistance> _resistances;


    public float HealthPoints { get; private set; }
    public float MoveSpeed { get; private set; }
    public float Acceleration { get; private set; }
    public float Deceleration { get; private set; }
    public float SwingSpeed { get; private set; }
    public float SwingSpeedup { get; private set; }
    public float SwingSlowdown { get; private set; }
    public float ProjectileSpeed { get; private set; }
    public float RateOfFire { get; private set; }
    public EquipmentHandler Equipment { get; private set; }
    public List<Resistance> Resistances => _resistances;

    public override void CalculateValues()
    {
        CalculateValuesInList(_attributes);
        CalculateValuesInList(_stats);
        CalculateValuesInList(_resistances);
        CalculateValuesInList(_damages);
    }
    public ShotStats GetShotStats()
    {
        var DamageTypeValueDict = new Dictionary<DamageType, float>();
        foreach (Damage damage in _damages)
        {
            try
            {
                DamageTypeValueDict.Add(damage.Type, damage.Value);
            }
            catch
            {
                throw new Exception($"Cant Add {damage} to {DamageTypeValueDict}");
            }
        }

        ShotDamage shotDamage = new ShotDamage(DamageTypeValueDict);

        return new ShotStats(shotDamage, transform, ProjectileSpeed);
    }
    public override List<StatModifier> GetAllModifiers(string targetStatName)
    {
        var modifierList = new List<StatModifier>();
        var relevantModifiers = new List<StatModifier>();

        modifierList.AddRange(AttributesMultModifiers());
        modifierList.AddRange(Equipment.GetAllModifiers());
        modifierList.AddRange(CurrentEnvironment.Modifiers);

        relevantModifiers = modifierList.FindAll(modifier => modifier.HasInfluenceToStat(targetStatName));

        var arrangeList = new List<StatModifier>(ArrangeModifiers(relevantModifiers));
        return arrangeList;
    }
    public override List<StatModifier> GetAllModifiers()
    {
        var modifierList = new List<StatModifier>();
        modifierList.AddRange(AttributesMultModifiers());
        modifierList.AddRange(Equipment.GetAllModifiers());
        modifierList.AddRange(CurrentEnvironment.Modifiers);
        Debug.Log(CurrentEnvironment.Modifiers.Count);
        var arrangeList = new List<StatModifier>(ArrangeModifiers(modifierList));

        return arrangeList;
    }

    protected override void OnValuesCalculated()
    {
        HealthPoints = GetStat(HEALTHPOINTS).Value;
        MoveSpeed = GetStat(MOVE_SPEED).Value;
        SwingSpeed = GetStat(SWING_SPEED).Value;
        Acceleration = GetStat(ACCELERATION).Value;
        Deceleration = GetStat(DECELERATION).Value;
        SwingSpeedup = GetStat(SWING_SPEEDUP).Value;
        SwingSlowdown = GetStat(SWING_SLOWDOWN).Value;
        RateOfFire = GetStat(RATE_OF_FIRE).Value;
        ProjectileSpeed = GetStat(SHOT_SPEED).Value;
    }
    protected List<StatModifier> AttributesMultModifiers()
    {
        List<StatModifier> modifiers = new List<StatModifier>(); ;
        foreach (Stats.Attribute attribute in _attributes)
        {
            var attributeModifiers = new List<StatModifier>(attribute.Modifiers);
            foreach (StatModifier modifier in attributeModifiers)
            {
                var multModifier = new StatModifier(modifier.Preset, (modifier.Value * attribute.Value));
                modifiers.Add(multModifier);
            }
            modifiers = new List<StatModifier>();
        }
        return modifiers;
    }
    public override void Initialize()
    {
        
        InitializeStats(_attributes);
        InitializeStats(_stats);
        InitializeStats(_resistances);
        InitializeStats(_damages);
        Equipment = MyTools.GetComponent<EquipmentHandler>(gameObject);
        CalculateValues();
        OnValuesCalculated();
    }
}

