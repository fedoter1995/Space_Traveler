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
    [RequireComponent(typeof(Collider))]
    public class TakeDamageHandler : MonoBehaviour, ITakeDamage
    {

        [SerializeField]
        private TriggerObjectType _triggerType;

        
        private List<Resistance> resistances;

        public Vector3 Position => transform.position;
        public IHaveTakeHitHandler Obj { get; private set; }

        public TriggerObjectType Type => _triggerType;

        public event Action<object, DamageTypeValue> OnTakeDamageEvent;
        public event Action<HitStats> OnTakeHitEvent;

        public void Initialize(IHaveTakeHitHandler obj, List<Resistance> resistances)
        {
            Obj = obj;
            SetResistances(resistances);
        }
        public void SetResistances(List<Resistance> resistances)
        {
            this.resistances = resistances;
        }
        public void TakeDamage(object sender, HitDamage damage)
        {
            HitDamage currentDamage = ApplyResistances(damage);

            foreach (DamageTypeValue dmg in currentDamage.DamageTypeValues)
            {
                TakeDamageMessage damageMessage = new TakeDamageMessage(this, dmg);

                OnTakeDamageEvent?.Invoke(sender, dmg);
            }
        }
        public void TakeHit(object sender, Hit hit)
        {
            var takenDamage = hit.GetHitDamage();
            TakeDamage(sender, takenDamage);
        }
        private HitDamage ApplyResistances(HitDamage damage)
        {
            var resultDictionary = new List<DamageTypeValue>();

            foreach (DamageTypeValue dmg in damage.DamageTypeValues)
            {
                var res = resistances.Find(res => res.Type == dmg.Type);

                if (res != null)
                {
                    var resultIntDamage = (int)(dmg.Value - dmg.Value * (res.Value / 100));
                    var newDamageValue = new DamageTypeValue(resultIntDamage, dmg.Type, dmg.IsCrit);

                    resultDictionary.Add(newDamageValue);
                }
                else
                    resultDictionary.Add(dmg);
            }
            var newHitDamage = new HitDamage(resultDictionary);


            return newHitDamage;
        }


    }
}


