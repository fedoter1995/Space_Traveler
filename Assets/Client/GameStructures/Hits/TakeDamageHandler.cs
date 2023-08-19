using System;
using System.Collections.Generic;
using UnityEngine;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Stats.Chances;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;
using Assets.Client.GameStructures.Stats.PackedStats;
using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Stats.PackedStats;

namespace SpaceTraveler.GameStructures.Hits
{
    public class TakeDamageHandler : TakeHitHandler
    {

        public event Action<DamageAttributes> OnTakeDamageEvent;
        public override event Action<HitStats> OnTakeHitEvent;

        private IHaveDefenciveStats handler;


        private List<Resistance> resistances => handler.GetResistances();
        private List<ActionChance> actionChances => handler.GetDefenciveActionStats();
        public void Initialize(IHaveDefenciveStats handler)
        {
            this.handler = handler;
        }


        public void TakeDamage(object sender, HitDamage damage)
        {

            HitDamage currentDamage = ApplyResistances(damage);

            foreach (DamageAttributes dmg in currentDamage.DamageTypeValues)
            {
                if(dmg.Value > 0)
                {

                    TakeDamageMessage damageMessage = new TakeDamageMessage(sender, gameObject, dmg);

                    Debug.Log(damageMessage);

                    OnTakeDamageEvent?.Invoke(dmg);
                }
            }         
        }
        public void TakeDamage(object sender, DamageAttributes damage)
        {

            DamageAttributes currentDamage = ApplyResistances(damage);

            if (currentDamage.Value > 0)
            {
                TakeDamageMessage damageMessage = new TakeDamageMessage(sender, gameObject, currentDamage);

                OnTakeDamageEvent?.Invoke(currentDamage);

                Debug.Log(damageMessage);
            }

        }
        public override void TakeHit(object sender, HitStats hitStats)
        {
            var takenDamage = CalculateDamage(hitStats);

            OnTakeHitEvent?.Invoke(hitStats);

            if(!takenDamage.IsZeroValue())
            {
                TakeDamage(sender, takenDamage);
            }

        }
        private HitDamage ApplyResistances(HitDamage damage)
        {
            if(resistances == null)
                Debug.LogError("Resistances is not installed");


            var resultDictionary = new List<DamageAttributes>();

            foreach (DamageAttributes dmg in damage.DamageTypeValues)
            {
                if(dmg.Value > 0)
                {
                    Resistance res = null;

                    if (resistances != null)
                        res = resistances.Find(res => res.Type == dmg.Type);
                    else
                        Debug.Log("Resistances is not installed");


                    if (res != null)
                    {
                        var resultIntDamage = (int)(dmg.Value - dmg.Value * (res.Value / 100));
                        var newDamageValue = new DamageAttributes(resultIntDamage, dmg.Type, dmg.IsCrit);

                        resultDictionary.Add(newDamageValue);
                    }
                    else
                        resultDictionary.Add(dmg);
                }
                
            }
            var newHitDamage = new HitDamage(resultDictionary);


            return newHitDamage;
        }
        private DamageAttributes ApplyResistances(DamageAttributes damage)
        {
            if (resistances == null)
                Debug.LogError("Resistances Handler is not installed");

            DamageAttributes currentDamage = damage;

            if (damage.Value > 0)
            {
                Resistance res = null;

                if (resistances != null)
                    res = resistances.Find(res => res.Type == damage.Type);
                else
                    Debug.Log("Resistances is not installed");


                if (res != null)
                {
                    var resultIntDamage = (int)(damage.Value - damage.Value * (res.Value / 100));
                    var newDamageValue = new DamageAttributes(resultIntDamage, damage.Type, damage.IsCrit);

                    currentDamage = newDamageValue;
                }

            }

            return currentDamage;
        }
        private HitDamage CalculateDamage(HitStats stats)
        {
            var resultDamage = CalculateCritDamage(stats.HitDamage, stats.MultStats);

            return resultDamage;
        }
        private HitDamage CalculateCritDamage(HitDamage damage, List<PackedMultStats> multStats)
        {
            var resultDamage = new List<DamageAttributes>();

            foreach (DamageAttributes dmg in damage.DamageTypeValues)
            {
                var randomValue = UnityEngine.Random.Range(0, 100.1f);


                var stats = multStats.Find(stat => stat.DamageType == dmg.Type);

                float mult = 1f;
                float ch = 0f;

                if (stats != null)
                {
                    var multiplier = stats.Multiplier;
                    var chance = stats.Chance;

                    mult = multiplier;
                    ch = chance;
                }


                if (ch >= randomValue)
                {
                    var newDamageInt = dmg.Value * mult;

                    var newDamageValue = new DamageAttributes((int)newDamageInt, dmg.Type, true);

                    resultDamage.Add(newDamageValue);
                }
                else
                {
                    resultDamage.Add(dmg);
                }

            }

            HitDamage resultHitDamage = new HitDamage(resultDamage);

            return resultHitDamage;
        }

    }

}


