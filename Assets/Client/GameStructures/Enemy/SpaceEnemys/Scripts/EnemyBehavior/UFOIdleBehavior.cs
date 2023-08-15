using System.Collections;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Enemys
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
