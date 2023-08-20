using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Stats.PackedStats;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Scripts
{
    public class ProtectiveComponentsHandler : MonoBehaviour
    {

        [SerializeField]
        private LastingEffectsHandler _lastingEffectsHandler = new LastingEffectsHandler();
        [SerializeField]
        private TakeDamageHandler _takeDamageHandler;

        private bool isEnabled = false;
        private object sender = null;

        #region Events
        public event Action OnTakeHitEvent;
        public event Action<object, DamageAttributes> OnTakeDamageEvent;
        #endregion

        private IHaveDefenciveStats handler;
        public void Initialize(IHaveDefenciveStats handler)
        {
            this.handler = handler;

            _takeDamageHandler.Initialize(handler);

            _lastingEffectsHandler.Initialize();

            _takeDamageHandler.OnTakeDamageEvent += OnTakeDamage;
            _takeDamageHandler.OnTakeHitEvent += OnTakeHit;
            _lastingEffectsHandler.OnDotTriggeredEvent += _takeDamageHandler.TakeDamage;

            isEnabled = true;
        }
        public void TakeHit(object sender, HitStats hitStats)
        {
            if(isEnabled)
            {
                this.sender = sender;
                _takeDamageHandler.TakeHit(sender, hitStats);
                this.sender = null;
            }
        }
        public void ApplyLastingEffect(LastingEffect lastingEffect)
        {

        }
        public void OnDeath()
        {
            isEnabled = false;
            _lastingEffectsHandler.OnDeath();
        }
        private void OnTakeDamage(DamageAttributes damageStats)
        {
            OnTakeDamageEvent?.Invoke(sender, damageStats);
        }
        private void OnTakeHit(HitStats hitStats)
        {
            CalculateDotEffects(hitStats.DotStats);

            OnTakeHitEvent?.Invoke();
        }
        private void CalculateDotEffects(List<PackedDotStats> dotStatsList)
        {
            foreach(var dotStats in dotStatsList)
            {
                var randomValue = UnityEngine.Random.Range(0, 100.1f);

                if (randomValue <= dotStats.Chance)
                {
                    var durationParameters = new DurationParameters(dotStats.Duration, dotStats.Frequency);

                    var dotEffectStats = new DotEffectStats(durationParameters, dotStats.Damage);
                    Debug.Log("add");
                    DOTEffect dot = new DOTEffect(sender, dotEffectStats);
                    _lastingEffectsHandler.AddDotEffect(dot);
                }
            }
        }


    }
}
