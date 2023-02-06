using Stats;
using System;
using System.Collections.Generic;
using UnityEngine;
public abstract class Weapon : Equipment
{
    [SerializeField]
    protected Projectile projectile;

    public abstract void Shot(ShotStats stats);
    public override List<StatModifier> GetAllModifiers()
    {
        var modifiers = new List<StatModifier>(_modifiers);
        modifiers.AddRange(projectile.GetAllModifiers());
        return modifiers;
    }
}

