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
        private PoolsParticles m_bloodParticles;
        [SerializeField]
        private CharacterAnimationEventsHandler m_eventsHandler;
        [SerializeField]
        private SpriteRenderer m_spriteRenderer;
        [SerializeField]
        private Transform _vfxParent;
        
        private int IntIdle = Animator.StringToHash("Idle");
        private int IntHurt = Animator.StringToHash("Hurt");
        private int IntDeath = Animator.StringToHash("Death");


        private Pool<PoolsParticles> bloodSplashes;
        public CharacterAnimationEventsHandler EventsHandler => m_eventsHandler;

        public override void Initialize()
        {
            base.Initialize();
            bloodSplashes = new Pool<PoolsParticles>(m_bloodParticles,5,transform, true);
            m_eventsHandler.HurtEndEvent += OnHurtEnd;
        }

        public void TakeDamageAnimation()
        {
            BloodAnimation();
            Play(IntHurt);
        }
        public void DeathAnimation()
        {
            Play(IntDeath);
        }
        private void BloodAnimation()
        {
            var particles = bloodSplashes.GetFreeObject();
            particles.transform.position = _vfxParent.position;
        }
        private void OnHurtEnd()
        {
            Play(IntIdle);
        }

    }
}
