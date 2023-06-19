using System;
using System.Collections.Generic;
using UnityEngine;
using GameStructures.Effects;
using GameStructures.Stats;

namespace GameStructures.Hits
{
    [Serializable]
    public class HitStats
    {
    
        [SerializeField]
        private List<Chance> chances;
        [SerializeField]
        private List<Multiplier> multipliers;
        [SerializeField]
        private HitDamage damage;
        private int numbOfPenetrations = 0;

        public List<Chance> Chances => chances;
        public List<Multiplier> Multipliers => multipliers;
        public HitDamage ShotDamage => damage;
        public int PenetrationsNumb => numbOfPenetrations;

        public HitStats(HitDamage damage, List<Chance> chances, List<Multiplier> multipliers, int numbOfPenetrations)
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

            this.numbOfPenetrations = numbOfPenetrations;
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
            numbOfPenetrations = stats.PenetrationsNumb;
        }

        public void OnHit()
        {
            numbOfPenetrations--;
        }


    }

}

