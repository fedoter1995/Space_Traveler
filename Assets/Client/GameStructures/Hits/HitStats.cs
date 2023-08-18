using System;
using System.Collections.Generic;
using UnityEngine;
using SpaceTraveler.GameStructures.Effects;

namespace SpaceTraveler.GameStructures.Stats
{
    [Serializable]
    public class HitStats
    {
    
        [SerializeField]
        private List<Chance> _chances;
        [SerializeField]
        private List<Multiplier> _multipliers;
        [SerializeField]
        private List<Effect> _effects;
        [SerializeField]
        private HitDamage _damage;


        private int numbOfPenetrations = 0;

        public List<Chance> Chances => _chances;
        public List<Multiplier> Multipliers => _multipliers;
        public HitDamage HitDamage => _damage;
        public int PenetrationsNumb => numbOfPenetrations;

        public HitStats(object sender, HitDamage damage, List<Chance> chances, List<Multiplier> multipliers, int numbOfPenetrations)
        {

            if(chances != null)
                this._chances = new List<Chance>(chances);
            else
                this._chances = new List<Chance>();
            if (multipliers != null)
                this._multipliers = new List<Multiplier>(multipliers);
            else
                this._multipliers = new List<Multiplier>();
            this._damage = damage;

            this.numbOfPenetrations = numbOfPenetrations;
        }
        public HitStats(object sender, HitDamage damage)
        {
            _chances = new List<Chance>();
            _multipliers = new List<Multiplier>();
            this._damage = damage;
        }
        public HitStats(HitStats stats)
        {
            if (_chances != null)
                this._chances = new List<Chance>(stats.Chances);
            else
                this._chances = new List<Chance>();
            if (_multipliers != null)
                this._multipliers = new List<Multiplier>(stats.Multipliers);
            else
                this._multipliers = new List<Multiplier>();
            _damage = stats.HitDamage;
            numbOfPenetrations = stats.PenetrationsNumb;
        }

        public void OnHit()
        {
            numbOfPenetrations--;
        }
    }

}

