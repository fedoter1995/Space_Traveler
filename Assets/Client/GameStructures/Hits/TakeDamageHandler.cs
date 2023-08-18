using System;
using System.Collections.Generic;
using UnityEngine;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Stats.Scripts;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;

namespace SpaceTraveler.GameStructures.Hits
{
    public class TakeDamageHandler : TakeHitHandler
    {

        public event Action<DamageAttributes> OnTakeDamageEvent;
        public override event Action OnTakeHitEvent;

        private IHaveDefenciveStats handler;


        private List<Resistance> resistances => handler.GetResistances();
        private List<ActionChance> actionChances => handler.GetDefenciveActionStats();
        public void Initialize(IHaveDefenciveStats handler)
        {
            this.handler = handler;
        }


        public void TakeDamage(HitDamage damage)
        {

            HitDamage currentDamage = ApplyResistances(damage);

            foreach (DamageAttributes dmg in currentDamage.DamageTypeValues)
            {
                if(dmg.Value > 0)
                {
                    TakeDamageMessage damageMessage = new TakeDamageMessage(this, dmg);

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
                TakeDamageMessage damageMessage = new TakeDamageMessage(this, currentDamage);

                OnTakeDamageEvent?.Invoke(currentDamage);

                Debug.Log(damageMessage);
            }

        }
        public override void TakeHit(HitStats hitStats)
        {


            var takenDamage = CalculateDamage(hitStats);

            OnTakeHitEvent?.Invoke();

            if(!takenDamage.IsZeroValue())
            {
                TakeDamage(takenDamage);
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

            var multipliers = stats.Multipliers.FindAll(multiplier => multiplier.MultiplierType == MultiplierType.DamageMultiplier);
            var currentChances = stats.Chances.FindAll(chance => chance.GetType() == typeof(MultiplierChance));

            var multChances = new List<MultiplierChance>();

            foreach ( var chance in currentChances)
            {
                var multChance = chance as MultiplierChance;

                multChances.Add(multChance);
            }


            var resultDamage = CalculateCritDamage(stats.HitDamage, multipliers, multChances);

            return resultDamage;
        }
        private HitDamage CalculateCritDamage(HitDamage damage, List<Multiplier> multipliers, List<MultiplierChance> chances)
        {
            var resultDamage = new List<DamageAttributes>();

            foreach (DamageAttributes dmg in damage.DamageTypeValues)
            {
                var randomValue = UnityEngine.Random.Range(0, 100.1f);


                var multiplier = multipliers.Find(item => item.DamageType == dmg.Type);
                var chance = chances.Find(item => item.MultiplierRef == multiplier.Preset);
                float mult = 1f;
                float ch = 0f;

                if (multiplier != null)
                    mult = multiplier.Value;

                if (chance != null)
                    ch = chance.Value;

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


