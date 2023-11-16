using Assets.Client.GameStructures.Stats.PackedStats;
using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Stats.Chances;
using SpaceTraveler.GameStructures.Stats.PackedStats;
using SpaceTraveler.GameStructures.Stats.StatModifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    public abstract class CombatStatsHandler : StatsHandler
    {
        [SerializeField, Header("Damages Stats")]
        protected List<Damage> _damages = new List<Damage>();


        [Header("======================================= Dot Stats =======================================")]
        [SerializeField]
        protected List<DotChance> _dotChances = new List<DotChance>();
        [SerializeField]
        protected List<DamageOverTime> _dotDamages = new List<DamageOverTime>();


        [Header("======================================= Lasting Stats =======================================")]
        [SerializeField]
        protected List<Duration> _durations = new List<Duration>();
        [SerializeField]
        protected List<Frequency> _frequencies = new List<Frequency>();


        [Header("======================================= Multipliers Stats =================================")]
        [SerializeField]
        protected List<Multiplier> _multipliers = new List<Multiplier>();
        [SerializeField]
        protected List<MultiplierChance> _multiplieChances = new List<MultiplierChance>();


        public HitStats GetHitStats(AddedModifiers addedModifiers = null)
        {
            
            if(addedModifiers != null)
            {
                CalculateValues(addedModifiers);
            }


            var damageAttributes = new List<DamageAttributes>();
            var damages = new List<Damage>(_damages);

            var packedMultStats = PackMultStats();
            var dotStats = PackDotStats();


            foreach (Damage damage in damages)
            {
                try
                {
                    var dmg = new DamageAttributes((int)damage.Value, damage.Type);
                    damageAttributes.Add(dmg);
                }
                catch
                {
                    throw new Exception($"Cant Add {damage} to {damageAttributes}");
                }
            }

            HitDamage hitDamage = new HitDamage(damageAttributes);

            var hitStats = new HitStats(mainObject, hitDamage, dotStats, packedMultStats, 0);

            CalculateValues();

            return hitStats;

        }
        private List<PackedDotStats> PackDotStats()
        {
            var damages = new List<DamageOverTime>(_dotDamages);
            var durations = new List<Duration>(_durations);
            var frequencies = new List<Frequency>(_frequencies);
            var dotStats = new List<PackedDotStats>();
            foreach (var chance in _dotChances)
            {
                Duration duration;
                DamageOverTime damage;
                Frequency frequency;
                try
                {
                    damage = damages.Find(dmg => dmg.DamageOverTimeRef == chance.ChancePreset.DamageOverTimeRef);
                    damages.Remove(damage);
                }
                catch (NullReferenceException ex)
                {
                    throw new ArgumentNullException("Cant find current DamageOverTime");
                }
                try
                {
                    duration = durations.Find(dur => dur.DurationPreset == chance.ChancePreset.DamageOverTimeDurationRef);
                    durations.Remove(duration);
                }
                catch (NullReferenceException ex)
                {
                    throw new ArgumentNullException("Cant find current DotDuration");
                }
                try
                {
                    frequency = frequencies.Find(freq => freq.Preset == chance.ChancePreset.FrequencyRef);
                    frequencies.Remove(frequency);
                }
                catch (NullReferenceException ex)
                {
                    throw new ArgumentNullException("Cant find current DotDuration");
                }

                var damageAttributes = new DamageAttributes((int)damage.Value, damage.DamageType);

                dotStats.Add(new PackedDotStats(damageAttributes, chance.Value, 1, (int)duration.Value));
            }

            return dotStats;
        }
        private List<PackedMultStats> PackMultStats()
        {
            var multipliers = new List<Multiplier>(_multipliers);
            var packedMultStats = new List<PackedMultStats>();

            foreach (var chance in _multiplieChances)
            {
                Multiplier multiplier;

                try
                {
                    multiplier = multipliers.Find(mult => mult.Preset == chance.MultiplierRef);
                    multipliers.Remove(multiplier);
                }
                catch (NullReferenceException ex)
                {
                    throw new ArgumentNullException("Cant find current DamageOverTime");
                }


                packedMultStats.Add(new PackedMultStats(chance.Value, multiplier.Value, multiplier.DamageType));
            }

            return packedMultStats;
        }
    }
}
