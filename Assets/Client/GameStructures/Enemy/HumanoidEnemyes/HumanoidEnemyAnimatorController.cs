using SpaceTraveler.GameStructures.Hits;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Enemy.HumanoidEnemyes
{
    public class HumanoidEnemyAnimatorController : AnimatorController
    {
        private int IntHurt = Animator.StringToHash("Hurt");
        private TakeDamageHandler handler;

        private void Awake()
        {
            handler = GetComponent<TakeDamageHandler>();

            handler.OnTakeHitEvent += OnTakeHit;
        }

        private void OnTakeHit()
        {
            SetTrigger(IntHurt);
        }
    }
}
