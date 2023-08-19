using CustomTools;
using CustomTools.Observable;
using GameStructures.Enemys;
using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Gear.Weapons;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Zones;
using SpaceTraveler.Scripts;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace SpaceTraveler.GameStructures.Enemys.SpaceEnemys
{
    [RequireComponent(typeof(SpaceEnemyController), typeof(LootRandomizer))]
    public class SpaceEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private SpaceEnemyStatsHandler _stats;
        [SerializeField]
        private ProtectiveComponentsHandler _protectiveComponents;
        [SerializeField]
        private SpaceEnemyAnimatorController _enemyAnimatorController;
        [SerializeField]
        private TriggerObjectType _type;

        private SpaceEnemyController controller;

        public event Action<object, DamageAttributes> OnTakeDamageEvent;
        public event Action OnTakeHitEvent;
        public event Action<int> HeathPointsChangedEvent;

        public string Name => _name;
        public NavMeshAgent Agent { get; private set; }
        public SpaceEnemyController Controller => controller;
        public SpaceEnemyStatsHandler StatsHandler => _stats;
        public Vector3 Position => transform.position;
        public Observable<int> CurrentHealthPoints { get; private set; }

        public TriggerObjectType Type => _type;


        private void Awake()
        {
            Initialize();
        }
        public void Initialize()
        {
            GetComponents();

            _enemyAnimatorController.OnEndDestoryAnimationEvent += SetActive;

            _stats.SetShootPoints(GetComponentsInChildren<ShootPosition>().ToList());
            _stats.Initialize(this);

            controller.Initialize(this);

            _protectiveComponents.Initialize(StatsHandler);
            _protectiveComponents.OnTakeDamageEvent += TakeDamage;

            if (CurrentHealthPoints == null)
                CurrentHealthPoints = new Observable<int>((int)_stats.HealthPoints);

        }
        private void GetComponents()
        {
            Agent = GetComponent<NavMeshAgent>();

            controller = GetComponent<SpaceEnemyController>();

            if (_protectiveComponents is null)
                throw new Exception("TakeDamageHandler is not installed");

        }
        public void TakeDamage(object sender, DamageAttributes damage)
        {

            CurrentHealthPoints.Value -= damage.Value;

            if (CurrentHealthPoints.Value <= 0)
                DestroyEnemy();

        }
        public void TakeHit(object sender, HitStats hitStats)
        {
            _protectiveComponents.TakeHit(sender, hitStats);

            var triggerObj = sender as ITriggerObject;

            if (triggerObj != null)
                Controller.OnTakeDamage(triggerObj);
        }
        public void Hit(ITakeHit target)
        {

            var hitDamage = new HitDamage(new DamageAttributes((int)_stats.HealthPoints, DamageType.Physical));


            target.TakeHit(this, new HitStats(this, hitDamage));

            DestroyEnemy();

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var target = collision.GetComponentInParent<ITakeHit>();

            if (target == null)
                target = collision.GetComponent<ITakeHit>();

            if (target != null)
            {
                Hit(target);
                return;
            }
        }
        private void SetActive(bool activity)
        {
            gameObject.SetActive(activity);
        }
        private void DestroyEnemy()
        {
            controller.OnDestroyObject();
            _enemyAnimatorController.PlayDestroyAnimation();
        }

    }
}

