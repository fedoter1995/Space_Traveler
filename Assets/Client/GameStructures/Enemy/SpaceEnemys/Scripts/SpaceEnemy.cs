using CustomTools;
using CustomTools.Observable;
using GameStructures.Effects;
using GameStructures.Gear;
using GameStructures.Gear.Weapons;
using GameStructures.Hits;
using GameStructures.Stats;
using GameStructures.Zones;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace GameStructures.Enemys
{
    [RequireComponent(typeof(SpaceEnemyController), typeof(LootRandomizer))]
    public class SpaceEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private SpaceEnemyStatsHandler _stats;
        [SerializeField]
        private TakeDamageHandler _takeDamageHandler;
        [SerializeField]
        public StatusEffectHandler _statusHandler;
        [SerializeField]
        private SpaceEnemyAnimatorController _enemyAnimatorController;

        private SpaceEnemyController controller;

        public event Action<object, DamageTypeValue> OnTakeDamageEvent;
        public event Action<HitStats> OnTakeHitEvent;

        public string Name => _name;
        public NavMeshAgent Agent { get; private set; }
        public Observable<bool> IsDestroyed { get; private set; } = new Observable<bool>(false);
        public Observable<int> HealthPoints { get; private set; }
        public SpaceEnemyController Controller => controller;
        public SpaceEnemyStatsHandler StatsHandler => _stats;

        public Vector3 Position => transform.position;

        public TakeHitHandler TakeHitHandler => _takeDamageHandler;
        public TakeDamageHandler TakeDamageHandler => _takeDamageHandler;

        public StatusEffectHandler StatusHandler => throw new NotImplementedException();

        private void Awake()
        {
            Initialize();
        }
        public void Initialize()
        {
            GetComponents();

            _enemyAnimatorController.OnEndDestoryAnimationEvent += SetActive;

            _stats.SetShootPoints(GetComponentsInChildren<ShootPosition>().ToList());
            _stats.Initialize();

            controller.Initialize(this);

            _takeDamageHandler.Initialize(this, StatsHandler);
            _takeDamageHandler.OnTakeDamageEvent += TakeDamage;

            if (HealthPoints == null)
                HealthPoints = new Observable<int>((int)_stats.HealthPoints);

        }
        private void GetComponents()
        {
            Agent = GetComponent<NavMeshAgent>();

            controller = GetComponent<SpaceEnemyController>();

            if (_takeDamageHandler is null)
                throw new Exception("TakeDamageHandler is not installed");

        }
        public void TakeDamage(object sender, DamageTypeValue damage)
        {

            HealthPoints.Value -= damage.Value;


            var triggerObj = sender as ITriggerObject;

            if(triggerObj != null)
                Controller.OnTakeDamage(triggerObj);

            if (HealthPoints.Value <= 0)
                DestroyEnemy(sender);

        }
        private void DestroyEnemy(object sender)
        {
            controller.OnDestroyObject();
            _enemyAnimatorController.PlayDestroyAnimation();
        }
        public void Hit(ITakeHit target)
        {

            var hitDamage = new HitDamage(new DamageTypeValue((int)_stats.HealthPoints, DamageType.Physical));

            var hit = new Hit(new HitStats(hitDamage));

            target.TakeHit(this, hit);

            DestroyEnemy(target);

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var target = collision.GetComponentInParent<ITakeHit>();

            if (target == null)
                target = collision.GetComponent<ITakeHit>();

            if (target != null && target != this)
            {
                Hit(target);
                return;
            }
        }
        private void SetActive(bool activity)
        {
            gameObject.SetActive(activity);
        }
    }
}

