using GameStructures.Zones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Controllers
{
    public abstract class CombatSpaceAgentController : BaseSpaceAgentController
    {
        [SerializeField]
        protected TriggerZone _fireZone;
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
