using GameStructures.Zones;
using System;
using System.Collections;
using UnityEngine;

namespace GameStructures.Enemy
{
    public class UFOShotBehavior : IEnemyBehavior
    {
        private EnemyShootController controller;
        private ITriggerObject target;
        public UFOShotBehavior(EnemyShootController controller, ITriggerObject target)
        {
            this.controller = controller;

            if (target == null)
                throw new Exception("Target is null");

            this.target = target;
        }

        public IEnumerator ActionRoutine()
        {
            while(true)
            {
                yield return new WaitForSeconds(1/ controller.Stats.RateOfFire);
                PerformAction();
            }
        }
        private void PerformAction()
        {   
            var dirrection = (target.Position - controller.transform.position).normalized;
            controller.transform.up = dirrection;
            controller.Shot(dirrection);
        }
    }
}
