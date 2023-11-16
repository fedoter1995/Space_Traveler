using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Stats.PackedStats;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Characters
{
    [RequireComponent(typeof(Collider2D),typeof(Rigidbody2D))]
    public class TakeHitHandler : MonoBehaviour
    {
        [SerializeField] private LastingEffectsHandler m_lastingEffectsHandler = new LastingEffectsHandler();
        [SerializeField] private TakeDamageHandler m_takeDamageHandler;

        [Header("Subject to effects")]
        [SerializeField] private Effect m_effects;


        private bool isEnabled = false;
        private object sender = null;
        private Collider2D _collider;
        private Rigidbody2D _body;


        #region Events
        public event Action<HitStats> TakeHitEvent;
        public event Action<object, DamageAttributes> TakeDamageEvent;
        #endregion

        private IHaveDefenciveStats handler;
        public void Initialize(IHaveDefenciveStats handler)
        {
            this.handler = handler;
            _collider = GetComponent<Collider2D>();
            _body = GetComponent<Rigidbody2D>();


            m_takeDamageHandler.Initialize(handler);

            m_lastingEffectsHandler.Initialize();

            m_takeDamageHandler.OnTakeDamageEvent += OnTakeDamage;
            m_lastingEffectsHandler.OnDotTriggeredEvent += m_takeDamageHandler.TakeDamage;

            isEnabled = true;
        }
        public void TakeHit(object sender, HitStats hitStats)
        {
            if(isEnabled)
            {
                this.sender = sender;
                OnTakeHit(hitStats);
                this.sender = null;
            }
        }
        public void OnDeath()
        {
            isEnabled = false;
            _body.bodyType = RigidbodyType2D.Static;
            _collider.isTrigger = true;
            m_lastingEffectsHandler.OnDeath();
            m_takeDamageHandler.OnTakeDamageEvent -= OnTakeDamage;
            m_lastingEffectsHandler.OnDotTriggeredEvent -= m_takeDamageHandler.TakeDamage;
        }
        private void OnTakeDamage(DamageAttributes damageStats) =>TakeDamageEvent?.Invoke(sender, damageStats); 
        private void OnTakeHit(HitStats hitStats)
        {

            CalculateDotEffects(hitStats.DotStats);

            var takenDamage = m_takeDamageHandler.CalculateDamage(hitStats);

            TakeHitEvent?.Invoke(hitStats);

            if (!takenDamage.IsZeroValue())
            {
                m_takeDamageHandler.TakeDamage(sender, takenDamage);
            }
            
        }
        private void CalculateDotEffects(List<PackedDotStats> dotStatsList)
        {
            if(isEnabled)
            {
                foreach(var dotStats in dotStatsList)
                {
                    var randomValue = UnityEngine.Random.Range(0, 100.1f);

                    if (randomValue <= dotStats.Chance)
                    {
                        var durationParameters = new DurationParameters(dotStats.Duration, dotStats.Frequency);

                        var dotEffectStats = new DotEffectStats(durationParameters, dotStats.Damage);
                        DOTEffect dot = new DOTEffect(sender, dotEffectStats);
                        m_lastingEffectsHandler.AddDotEffect(dot);
                    }
                }
            }

        }
    }
}
