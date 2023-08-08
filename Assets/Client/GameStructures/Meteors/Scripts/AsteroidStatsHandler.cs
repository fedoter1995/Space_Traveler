using System;
using System.Collections.Generic;
using GameStructures.Stats;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "StatsHandler/AsteroidStats")]
public class AsteroidStatsHandler : StatsHandler, IHaveResistances
{

    #region Const
    private const string HEALTH = "Max_Health_Points";
    #endregion

    [SerializeField, Header("Stats")]
    private List<Stat> _stats;
    [SerializeField, Header("Damages")]
    private List<Damage> _damages;
    [SerializeField, Header("Resistance")]
    private List<Resistance> _resistances;

    public List<Resistance> Resistances => _resistances;
    public int HealthPoints { get; private set; }

    public override void Initialize()
    {
        InitializeStats(_stats);
        InitializeStats(_resistances);
        InitializeStats(_damages);
        CalculateValues();
        OnValuesCalculated();
    }

    public void AsteroidStatsInitialize()
    {
        Initialize();
    }
    public override void CalculateValues()
    {
        CalculateValuesInList(_stats);
        CalculateValuesInList(_resistances);
        CalculateValuesInList(_damages);
    }
    public HitDamage GetShotDamage()
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

        return shotDamage;
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
    public override List<StatModifier> GetAllModifiers()
    {
        var modifierList = new List<StatModifier>();

        modifierList.AddRange(CurrentEnvironment.Modifiers);

        var arrangeList = new List<StatModifier>(ArrangeModifiers(modifierList));

        return arrangeList;
    }
    protected override void OnValuesCalculated()
    {
        HealthPoints = (int)GetStat(HEALTH).Value;
    }

    public override BaseStat GetStat(string statName)
    {
        var stats = new List<BaseStat>(_stats);
        stats.AddRange(_damages);
        stats.AddRange(_resistances);

        return stats.Find(stat => stat.Name == statName);
    }

    public List<Resistance> GetResistances()
    {
        CalculateValues(); 
        return _resistances;
        
    }
}

