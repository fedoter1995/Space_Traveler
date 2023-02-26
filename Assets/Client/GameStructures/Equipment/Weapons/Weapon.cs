using GameStructures.Stats;
using Stats;
using System;
using System.Collections.Generic;
using UnityEngine;
public abstract class Weapon : Equipment
{
    [SerializeField]
    protected Projectile projectile;

    public abstract void Shot(ShotStats shotStats, HitStats hitStats);
    public override List<StatModifier> GetAllModifiers()
    {
        var modifiers = new List<StatModifier>(_modifiers);
        modifiers.AddRange(projectile.GetAllModifiers());
        return modifiers;
    }



    public override DescriptionData GetDescriptionData()
    {
        var footer = new List<string>();

        foreach (StatModifier modifier in _modifiers)
            footer.AddRange(modifier.GetDescriptionData());

        var data = new DescriptionData(Description, Name, footer, Icon);
        return data;
    }
}

