using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStructures.Enemys
{
    public class UFOIdleBehavior : IEnemyBehavior
    {
        public  IEnumerator ActionRoutine()
        {
                yield return new WaitForSeconds(1f);
                PerformAction();
        }
        private void PerformAction()
        {
            Debug.Log("I am in Idle");
        }
    }
}
