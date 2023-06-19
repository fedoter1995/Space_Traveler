using GameStructures.Stats;
using GameStructures.Zones;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers
{
    public abstract class BaseSpaceAgentController : MonoBehaviour
    {
        [SerializeField]
        protected TriggerZone _viewZone;

        private BaseSpaceStatsHandler statsHandler;
        private NavMeshAgent navMeshAgent;

        protected bool isInitialize = false;

        protected Coroutine actionRoutine;


        public void SetDestination(ITriggerObject trigger)
        {
            navMeshAgent.SetDestination(trigger.Position);
        }
        public virtual void OnDestroyObject()
        {
            _viewZone.OnChangeStateEvent -= ChangeControllerAction;
            StopAllCoroutines();
            SetAgentMoving(false);

        }
        protected void Initialize(NavMeshAgent agent, BaseSpaceStatsHandler statsHandler)
        {
            navMeshAgent = agent;
            this.statsHandler = statsHandler;

            this.statsHandler.OnCalculateValuesEvent += UpdateAgent;
            InitAgent();
            InitZones();
            isInitialize = true;
        }
        protected virtual void InitZones()
        {

            _viewZone.OnChangeStateEvent += ChangeControllerAction;
        }
        protected void SetAgentMoving(bool isMoving)
        {
            navMeshAgent.isStopped = !isMoving;
        }
        protected abstract void IdleAction();
        protected abstract void FollowAction(ITriggerObject obj);
        protected abstract void ChangeControllerAction();
        protected abstract void StartNewAction();
        private void InitAgent()
        {
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
            UpdateAgent();
        }
        private void UpdateAgent()
        {
            navMeshAgent.speed = statsHandler.MoveSpeed;
        }
    }
}
