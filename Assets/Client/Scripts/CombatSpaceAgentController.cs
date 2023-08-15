using SpaceTraveler.GameStructures.Zones;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Enemys.SpaceEnemys
{
    public abstract class CombatSpaceAgentController : BaseSpaceAgentController
    {
        [SerializeField]
        protected EnemyTriggerZone _fireZone;
        protected abstract void ShootAction(ITriggerObject obj);
        public override void OnDestroyObject()
        {
            _fireZone.OnChangeStateEvent -= ChangeControllerAction;
            _viewZone.OnChangeStateEvent -= ChangeControllerAction;

            StopAllCoroutines();

            SetAgentMoving(false);

        }
        protected override void InitZones()
        {

            _fireZone.OnChangeStateEvent += ChangeControllerAction;

            base.InitZones();
        }
    }
}
