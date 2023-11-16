using CustomTools.Observable;
using DG.Tweening;
using SpaceTraveler.Characters.HumanoidEnemyes.HumanoidEnemyStateMachine;
using SpaceTraveler.Characters.Player.PlayerFiniteStateMachine;
using SpaceTraveler.GameStructures.Characters.Player;
using SpaceTraveler.GameStructures.Enemys;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Zones;
using System;
using System.Collections;
using UnityEngine;
namespace SpaceTraveler.GameStructures.Characters.HumanoidEnemyes
{
    [RequireComponent(typeof(TakeHitHandler), typeof(HumanoidEnemyController))]
    public class HumanoidEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField]
        private TakeHitHandler _takeHitHandler;
        [SerializeField]
        private HumanoidEnemyAnimatorController _animatorController;
        [SerializeField]
        private ActorStatsHandler _statsHandler;
        [SerializeField]
        private HumanoidEnemyController _controller;
        [SerializeField]
        private CharacterAudioController _charactersAudioController;

        public event Action<int> HeathPointsChangedEvent;
        public event Action OnTakeHitEvent;
        public event Action<object, DamageAttributes> OnTakeDamageEvent;

        public Observable<int> CurrentHealthPoints { get; private set; }

        public TriggerObjectType Type => TriggerObjectType.Enemy;
        public Vector3 Position => transform.position;
        public EnemyStateMachine StateMachine { get; private set; }
        public ActorStatsHandler StatsHandler => _statsHandler;
        public HumanoidEnemyController Controller => _controller;
        public HumanoidEnemyAnimatorController AnimatorController => _animatorController;
        public CharacterAudioController AudioController => _charactersAudioController;
        public TakeHitHandler TakeHitHandler => _takeHitHandler;
        #region Enemy States
        public EnemyIdleState IdleState { get; private set; }
        #endregion

        public void TakeHit(object sender, HitStats hitStats)
        {
            _takeHitHandler.TakeHit(sender, hitStats);
        }
        public void TakeDamage(object sender, DamageAttributes damage)
        {
            if (damage.Value <= 0)
                return;

            CurrentHealthPoints.Value -= damage.Value;

            OnTakeDamageEvent?.Invoke(sender, damage);

            _animatorController.TakeDamageAnimation();

            if (CurrentHealthPoints.Value <= 0 )
            {
                OnDeath();
            }
        }


        private void Awake()
        {
            _statsHandler.Initialize(this);
            _takeHitHandler.Initialize(_statsHandler);
            _animatorController.Initialize();
            
            InitializeStates();

            StateMachine = new EnemyStateMachine();
            StateMachine.Initialize(IdleState);


            CurrentHealthPoints = new Observable<int>((int)_statsHandler.HealthPoints);

            _takeHitHandler.TakeHitEvent += OnTakeHit;
            _takeHitHandler.TakeDamageEvent += TakeDamage;
            _animatorController.EventsHandler.DeathEndEvent += DestroyEnemy;
        }

        private void DestroyEnemy()
        {
            StartCoroutine(DestroyEnemyRoutine());
        }

        private IEnumerator DestroyEnemyRoutine()
        {
            yield return new WaitForSeconds(1);
            var endPosition = transform.position - Vector3.up;
            transform.DOMove(endPosition, 2f).OnComplete(() => Destroy(gameObject));
        }
        
        private void OnReceivingheal(object sender, HealAttributes heal)
        {
            if (heal.Value <= 0)
                return;

            CurrentHealthPoints.Value += heal.Value;

        }
        private void OnDeath()
        {
            _takeHitHandler.OnDeath();
            _animatorController.DeathAnimation();
        }

        private void OnTakeHit(HitStats stats)
        {
            
        }
        private void InitializeStates()
        {
            IdleState = new EnemyIdleState(this);
        }
    }
}

