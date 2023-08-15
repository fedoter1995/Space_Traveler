using SpaceTraveler.GameStructures.Enemys.SpaceEnemys;
using SpaceTraveler.GameStructures.Zones;
using UnityEngine;
using UnityEngine.AI;

namespace SpaceTraveler.GameStructures.Enemys
{
    [RequireComponent(typeof(SpaceEnemyShootController), typeof(NavMeshAgent))]
    public class SpaceEnemyController : CombatSpaceAgentController
    {

        private SpaceEnemy enemy;
        private SpaceEnemyShootController shootController;
        private IEnemyBehavior enemyBehavior;
        private SpaceEnemyStatsHandler Stats => enemy.StatsHandler;

        public SpaceEnemyShootController ShootController => shootController;
        public IEnemyBehavior EnemyBehavior => enemyBehavior;


        public void Initialize(SpaceEnemy enemy)
        {
            shootController = GetComponent<SpaceEnemyShootController>();
            shootController.Initialize(enemy);

            this.enemy = enemy;
            Initialize(enemy.Agent, Stats);
            isInitialize = true;

            ChangeControllerAction();
        }
        protected override void IdleAction()
        {
            enemyBehavior = new UFOIdleBehavior();
            StartNewAction();           
        }
        protected override void ShootAction(ITriggerObject obj)
        {
            enemyBehavior = new UFOShotBehavior(shootController, obj);
            StartNewAction();
        }
        protected override void FollowAction(ITriggerObject obj)
        {
            SetAgentMoving(true);
            enemyBehavior = new UFOFollowBehavior(this, obj);
            StartNewAction();
        }
        protected override void ChangeControllerAction()
        {
            SetAgentMoving(false);

            if (actionRoutine != null)
                StopCoroutine(actionRoutine);

            if (_fireZone.ActiveObject != null)
                ShootAction(_fireZone.ActiveObject);
            else if (_viewZone.ActiveObject != null)
                FollowAction(_viewZone.ActiveObject);
            else
                IdleAction();
        }

        protected override void StartNewAction()
        {
            actionRoutine = StartCoroutine(enemyBehavior.ActionRoutine());
        }

        public void OnTakeDamage(ITriggerObject obj)
        {
            if (_fireZone.HaveObjectInZone(obj))
                ShootAction(obj);
            else
                FollowAction(obj);
        }
    }
}
