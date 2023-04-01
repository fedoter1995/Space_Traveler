using System;
using System.Collections.Generic;
using GameStructures.Stats;
using UnityEngine;

[Serializable]
public class AsteroidStatsHandler : StatsHandler
{

    #region Const
    private const string HEALTH = "Max_Health_Points";
    private const string POINT_PRICE = "Price";
    #endregion


    [SerializeField, Header("Damages")]
    private List<Damage> _damages;
    [SerializeField, Header("Resistance")]
    private List<Resistance> _resistances;

    public List<Resistance> Resistances => _resistances;
    public int HealthPoints { get; private set; }
    public int PointPrice { get; private set; }

    public override void Initialize()
    {
        InitializeStats(_stats);
        InitializeStats(_resistances);
        InitializeStats(_damages);
        CalculateValues();
        OnValuesCalculated();
    }
    public override void CalculateValues()
    {
        CalculateValuesInList(_stats);
        CalculateValuesInList(_resistances);
        CalculateValuesInList(_damages);
    }
    public HitDamage GetShotDamage()
    {
        var DamageTypeValueDict = new Dictionary<DamageType, DamageValue>();
        foreach (Damage damage in _damages)
        {
            try
            {
                var dmgValue = new DamageValue((int)damage.Value);
                DamageTypeValueDict.Add(damage.Type, dmgValue);
            }
            catch
            {
                throw new Exception($"Cant Add {damage} to {DamageTypeValueDict}");
            }
        }

        HitDamage shotDamage = new HitDamage(DamageTypeValueDict);

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
        PointPrice = (int)GetStat(POINT_PRICE).Value;
    }
}

