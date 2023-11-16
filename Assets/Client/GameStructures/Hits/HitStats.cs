using System;
using System.Collections.Generic;
using UnityEngine;
using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Stats.PackedStats;
using Assets.Client.GameStructures.Stats.PackedStats;

namespace SpaceTraveler.GameStructures.Stats
{
    [Serializable]
    public class HitStats
    {
    
        [SerializeField]
        private List<PackedDotStats> _dotStats;
        [SerializeField]
        private List<PackedMultStats> _packedMultStats;
        [SerializeField]
        private List<Effect> _effects;
        [SerializeField]
        private HitDamage _damage;


        private int numbOfPenetrations = 0;

        public List<PackedDotStats> DotStats => _dotStats;
        public List<PackedMultStats> MultStats => _packedMultStats;
        public HitDamage HitDamage => _damage;
        public int PenetrationsNumb => numbOfPenetrations;

        public HitStats(object sender, HitDamage damage, List<PackedDotStats> dotStats, List<PackedMultStats> packedMultStats, int numbOfPenetrations)
        {

            if(dotStats != null)
                this._dotStats = dotStats;
            else
                this._dotStats = new List<PackedDotStats>();


            if (packedMultStats != null)
                this._packedMultStats = new List<PackedMultStats>(packedMultStats);
            else
                this._packedMultStats = new List<PackedMultStats>();
            this._damage = damage;

            this.numbOfPenetrations = numbOfPenetrations;
        }
        public HitStats(object sender, HitDamage damage)
        {
            _dotStats = new List<PackedDotStats>();
            _packedMultStats = new List<PackedMultStats>();
            this._damage = damage;
        }
        public HitStats(HitStats stats)
        {
            if (_dotStats != null)
                this._dotStats = new List<PackedDotStats>(stats.DotStats);
            else
                this._dotStats = new List<PackedDotStats>();

            if (_packedMultStats != null)
                this._packedMultStats = new List<PackedMultStats>(stats.MultStats);
            else
                this._packedMultStats = new List<PackedMultStats>();
            
            _damage = stats.HitDamage;
            numbOfPenetrations = stats.PenetrationsNumb;
        }

        public void OnHit()
        {
            numbOfPenetrations--;
        }
    }

}

