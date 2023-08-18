using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Stats;
using System;
using UnityEngine;

namespace SpaceTraveler.Scripts
{
    public class ProtectiveComponentsHandler : MonoBehaviour
    {

        [SerializeField]
        private LastingEffectsHandler _lastingEffectsHandler;
       
        private TakeDamageHandler takeDamageHandler;

        private object sender = null;

        #region Events
        public event Action OnTakeHitEvent;
        public event Action<object, DamageAttributes> OnTakeDamageEvent;
        #endregion

        private IHaveDefenciveStats handler;
        public void Initialize(IHaveDefenciveStats handler)
        {
            this.handler = handler;

            takeDamageHandler = new TakeDamageHandler();
            takeDamageHandler.Initialize(handler);

            _lastingEffectsHandler.Initialize();

            takeDamageHandler.OnTakeDamageEvent += OnTakeDamage;
            takeDamageHandler.OnTakeHitEvent += OnTakeHitEvent;
            _lastingEffectsHandler.OnDotTriggeredEvent += takeDamageHandler.TakeDamage;
        }
        public void TakeHit(object sender, HitStats hitStats)
        {
            this.sender = sender;
            OnTakeHitEvent?.Invoke();
            takeDamageHandler.TakeHit(hitStats);
            sender = null;
        }
        public void ApplyLastingEffect(LastingEffect lastingEffect)
        {

        }

        private void OnTakeDamage(DamageAttributes damageStats)
        {
            Debug.Log("take damage");
            OnTakeDamageEvent?.Invoke(sender, damageStats);
        }

    }
}
