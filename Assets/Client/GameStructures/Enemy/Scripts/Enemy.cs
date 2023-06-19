using CustomTools;
using CustomTools.Observable;
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

namespace GameStructures.Enemy
{
    [RequireComponent(typeof(EnemyController), typeof(LootRandomizer))]
    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private EnemyStatsHandler _stats;
        [SerializeField]
        private TakeDamageHandler _takeHitHandler;
        [SerializeField]
        private EnemyAnimatorController _enemyAnimatorController;

        private EnemyController controller;

        public event Action<object, DamageTypeValue> OnTakeDamageEvent;
        public event Action<HitStats> OnTakeHitEvent;

        public string Name => _name;
        public NavMeshAgent Agent { get; private set; }
        public Observable<bool> IsDestroyed { get; private set; } = new Observable<bool>(false);
        public Observable<int> HealthPoints { get; private set; }
        public EnemyController Controller => controller;
        public EnemyStatsHandler StatsHandler => _stats;

        public Vector3 Position => transform.position;

        public TakeDamageHandler TakeHitHandler => _takeHitHandler;

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

            _takeHitHandler.Initialize(this, StatsHandler.Resistances);
            _takeHitHandler.OnTakeDamageEvent += TakeDamage;

            if (HealthPoints == null)
                HealthPoints = new Observable<int>((int)_stats.HealthPoints);

        }
        private void GetComponents()
        {
            Agent = GetComponent<NavMeshAgent>();

            controller = GetComponent<EnemyController>();

            if (_takeHitHandler is null)
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

