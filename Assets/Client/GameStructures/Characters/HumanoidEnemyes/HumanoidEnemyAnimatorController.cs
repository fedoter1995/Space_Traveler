using CustomTools;
using DG.Tweening;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.VFX;
using System.Collections;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Characters.HumanoidEnemyes
{
    public class HumanoidEnemyAnimatorController : AnimatorController
    {
        [SerializeField]
        private PoolsParticles _bloodParticles;
        [SerializeField]
        private Transform _vfxParent;

        private int IntHurt = Animator.StringToHash("Hurt");
        private int IntDeath = Animator.StringToHash("Death");


        private Pool<PoolsParticles> bloodSplashes;


        public void Initialize()
        {
            bloodSplashes = new Pool<PoolsParticles>(_bloodParticles,5,transform, true);
        }

        public void TakeHitAnimation()
        {
            BloodAnimation();
            SetTrigger(IntHurt);

        }
        public void DeathAnimation()
        {
            SetTrigger(IntDeath);
        }
        private void BloodAnimation()
        {
            var particles = bloodSplashes.GetFreeObject();
            particles.transform.position = _vfxParent.position;
        }


    }
}
