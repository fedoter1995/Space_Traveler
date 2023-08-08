using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using CustomTools.Observable;
using GameStructures.Stats;
using GameStructures.Hits;
using GameStructures.Zones;

namespace GameStructures.Hits
{
    [RequireComponent(typeof(Collider2D))]
    public class TakeDamageHandler : TakeHitHandler, ITakeDamage
    {

        private IHaveResistances resistancesHandler;


        public event Action<object, DamageTypeValue> OnTakeDamageEvent;
        public override event Action OnTakeHitEvent;
        public void Initialize(IHaveTakeHitHandler obj, IHaveResistances resistancesHandler)
        {
            Obj = obj;
            this.resistancesHandler = resistancesHandler;
        }
        public void TakeDamage(object sender, HitDamage damage)
        {
            HitDamage currentDamage = ApplyResistances(damage);

            foreach (DamageTypeValue dmg in currentDamage.DamageTypeValues)
            {
                if(dmg.Value > 0)
                {
                    TakeDamageMessage damageMessage = new TakeDamageMessage(this, dmg);

                    OnTakeDamageEvent?.Invoke(sender, dmg);
                
                    Debug.Log(damageMessage);
                }

            }
            
        }
        public override void TakeHit(object sender, Hit hit)
        {
            var takenDamage = hit.GetHitDamage();

            if(!takenDamage.IsZeroValue())
            {
                TakeDamage(sender, takenDamage);
            }

            OnTakeHitEvent?.Invoke();
        }
        private HitDamage ApplyResistances(HitDamage damage)
        {
            var resultDictionary = new List<DamageTypeValue>();
            var resistances = resistancesHandler.GetResistances();
            foreach (DamageTypeValue dmg in damage.DamageTypeValues)
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
                        var newDamageValue = new DamageTypeValue(resultIntDamage, dmg.Type, dmg.IsCrit);

                        resultDictionary.Add(newDamageValue);
                    }
                    else
                        resultDictionary.Add(dmg);
                }
                
            }
            var newHitDamage = new HitDamage(resultDictionary);


            return newHitDamage;
        }


    }
}


