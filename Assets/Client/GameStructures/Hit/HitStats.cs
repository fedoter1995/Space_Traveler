using System;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using GameStructures.Effects;
using GameStructures.Stats;

public class HitStats
{
    
    private List<Chance> chances;
    private List<Multiplier> multipliers;
    private HitDamage damage;


    public List<Chance> Chances => chances;
    public List<Multiplier> Multipliers => multipliers;
    public HitDamage ShotDamage => damage;

    public HitStats(HitDamage damage, List<Chance> chances, List<Multiplier> multipliers)
    {
        if(chances != null)
            this.chances = new List<Chance>(chances);
        else
            this.chances = new List<Chance>();
        if (multipliers != null)
            this.multipliers = new List<Multiplier>(multipliers);
        else
            this.multipliers = new List<Multiplier>();
        this.damage = damage;
    }
    public HitStats(HitDamage damage)
    {
        chances = new List<Chance>();
        multipliers = new List<Multiplier>();
        this.damage = damage;
    }
    public HitStats(HitStats stats)
    {
        if (chances != null)
            this.chances = new List<Chance>(stats.Chances);
        else
            this.chances = new List<Chance>();
        if (multipliers != null)
            this.multipliers = new List<Multiplier>(stats.Multipliers);
        else
            this.multipliers = new List<Multiplier>();
        damage = stats.ShotDamage;
    }
}

