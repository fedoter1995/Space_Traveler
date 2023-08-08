using GameStructures.Zones;
using System;
using System.Collections;
using UnityEngine;

namespace GameStructures.Enemys
{
    public class UFOFollowBehavior : IEnemyBehavior
    {
        private SpaceEnemyController controller;
        private ITriggerObject target;
        public UFOFollowBehavior(SpaceEnemyController controller, ITriggerObject target)
        {
            this.controller = controller;

            if (target == null)
            {
                throw new Exception("Target is null");
            }


            this.target = target;
        }
        
        public IEnumerator ActionRoutine()
        {
            PerformAction();
            while (true)
            {
                yield return new WaitForSeconds(1f);
                PerformAction();
            }

        }

        private void PerformAction()
        {
            controller.SetDestination(target);
        }

    }
}
