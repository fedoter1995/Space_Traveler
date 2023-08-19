using SpaceTraveler.GameStructures.Hits;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Characters.HumanoidEnemyes
{
    public class HumanoidEnemyAnimatorController : AnimatorController
    {
        [SerializeField]
        private ParticleSystem _bloodParticles;


        private int IntHurt = Animator.StringToHash("Hurt");
        private int IntDeath = Animator.StringToHash("Death");


        public void Initialize()
        {
        }

        public void TakeHitAnimation()
        {
            _bloodParticles.Play();
            SetTrigger(IntHurt);

        }
        public void DeathAnimation()
        {
            SetTrigger(IntDeath);
        }
        public void OnParticleSystemStopped()
        {
            _bloodParticles.Stop();
        }
    }
}
