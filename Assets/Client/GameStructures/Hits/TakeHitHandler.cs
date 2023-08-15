using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Zones;
using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Hits
{
    [RequireComponent(typeof(StatusEffectHandler), typeof(Collider2D))]
    public abstract class TakeHitHandler : MonoBehaviour, ITakeHit
    {
        [SerializeField]
        private TriggerObjectType _triggerType;

        private StatusEffectHandler statusHandler;

        public IHaveTakeHitHandler Obj { get; protected set; }

        public TriggerObjectType Type => _triggerType;

        public Vector3 Position => transform.position;

        public event Action OnTakeHitEvent;

        public virtual void TakeHit(object sender, Hit hit)
        {
            var takenDamage = hit.GetHitDamage();

            OnTakeHitEvent?.Invoke();
        }
    }
}