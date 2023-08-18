using SpaceTraveler.GameStructures.Hits;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Characters.HumanoidEnemyes
{
    public class HumanoidEnemyAnimatorController : AnimatorController
    {
        private int IntHurt = Animator.StringToHash("Hurt");
        private int IntDeath = Animator.StringToHash("Death");
        public void TakeHitAnimation()
        {
            SetTrigger(IntHurt);
        }
        public void DeathAnimation()
        {
            SetTrigger(IntDeath);
        }
    }
}
