using SpaceTraveler.GameStructures.Enemys.SpaceEnemys;
using SpaceTraveler.GameStructures.Zones;
using System;
using System.Collections;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Enemys
{
    public class UFOShotBehavior : IEnemyBehavior
    {
        private SpaceEnemyShootController controller;
        private ITriggerObject target;
        public UFOShotBehavior(SpaceEnemyShootController controller, ITriggerObject target)
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
