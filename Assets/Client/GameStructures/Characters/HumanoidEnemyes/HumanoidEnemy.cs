using CustomTools.Observable;
using SpaceTraveler.GameStructures.Enemys;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Zones;
using SpaceTraveler.Scripts;
using System;
using UnityEngine;
namespace SpaceTraveler.GameStructures.Characters.HumanoidEnemyes
{
    [RequireComponent(typeof(ProtectiveComponentsHandler), typeof(HumanoidEnemyAnimatorController), typeof(HumanoidEnemyAnimatorController))]
    public class HumanoidEnemy : MonoBehaviour, IEnemy
    {

        [SerializeField]
        private ProtectiveComponentsHandler _protectiveComponentsHandler;
        [SerializeField]
        private HumanoidEnemyAnimatorController _animatorController;
        [SerializeField]
        private MeleHumanoidEnemyStatsHandler _statsHandler;
        [SerializeField]
        private HumanoidEnemyController _controller;


        public Observable<int> CurrentHealthPoints { get; private set; }

        public TriggerObjectType Type => TriggerObjectType.Enemy;
        public Vector3 Position => transform.position;


        public event Action<int> HeathPointsChangedEvent;
        public event Action OnTakeHitEvent;
        public void TakeHit(object sender, HitStats hitStats)
        {
            _protectiveComponentsHandler.TakeHit(sender, hitStats);
        }
        private void OnTakeDamage(object sender, DamageAttributes damage)
        {
            if (damage.Value <= 0)
                return;

            CurrentHealthPoints.Value -= damage.Value;

            if (CurrentHealthPoints.Value <= 0 )
            {
                OnDeath();
            }

            Debug.Log(CurrentHealthPoints.Value);
        }

        private void OnReceivingheal(object sender, HealAttributes heal)
        {
            if (heal.Value <= 0)
                return;

            CurrentHealthPoints.Value += heal.Value;

        }
        private void OnDeath()
        {
            _animatorController.DeathAnimation();
        }

        private void Awake()
        {                     
            _statsHandler.Initialize(this);
            _protectiveComponentsHandler.Initialize(_statsHandler);

            CurrentHealthPoints = new Observable<int>((int)_statsHandler.MaxHealthPoints);

            _protectiveComponentsHandler.OnTakeHitEvent += _animatorController.TakeHitAnimation;
            _protectiveComponentsHandler.OnTakeDamageEvent += OnTakeDamage;
        }

    }
}
