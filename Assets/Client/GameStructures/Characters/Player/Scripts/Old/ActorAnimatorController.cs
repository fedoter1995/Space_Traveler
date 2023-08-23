using SpaceTraveler.GameStructures.Characters;
using SpaceTraveler.Audio;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.Scripts;
using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Characters.Player
{
    public class ActorAnimatorController : AnimatorController
    {
        [SerializeField]
        private CharactersAudioController _characterAudioController;
        [SerializeField]
        private CharacterAnimationEventsHandler _triggerHandler;
        
        private ActorController controller;


        #region Hash Animator Var
        private int IntStance = Animator.StringToHash("Stance");
        private int IntJump = Animator.StringToHash("Jump");
        private int IntLanding = Animator.StringToHash("Landing");
        private int IntAttack1 = Animator.StringToHash("Attack1");
        private int IntAttack2 = Animator.StringToHash("Attack2");
        private int IntIsBlock = Animator.StringToHash("IsBlock");
        private int IntMovement = Animator.StringToHash("HorizontalMovement");
        private int IntOnGround = Animator.StringToHash("OnGround");
        private int IntHurt = Animator.StringToHash("Hurt");
        #endregion

        public event Action<int> AttackTriggerEvent;
        public void Initialize(ActorController controller)
        {
            this.controller = controller;

            controller.GroundCheckHandler.GroundTypeChangeEvent += GroundTypeChange;
            controller.GroundCheckHandler.OnGroundStateChangeEvent += ChangeOnGroundState;
            controller.GroundCheckHandler.LandingEvent += TriggerLandingAnimation;

            _triggerHandler.EndAttackTriggerEvent += controller.CombatController.OnEndAttackTrigger;

            controller.JumpEvent += Jump;
            controller.ChangeStanceEvent += ChangeStance;
            controller.Attack1Event += OnAttack1;
            controller.Attack2Event += OnAttack2;
            controller.BlockStateChangeEvent += BlockStateChange;
            controller.OnMoveStateChangeEvent += WalkAnimation;
            _triggerHandler.BeginAttackTriggerEvent += SlashSound;
            _triggerHandler.StepEvent += _characterAudioController.OnStep;
        }
        public void TakeDamageAnimation(DamageAttributes stats)
        {
            SetTrigger(IntHurt);
        }
        private void GroundTypeChange(GroundSettings settings)
        {
            _characterAudioController.ChangeGroundSettings(settings.AudioSettings);
        }
        private void TriggerLandingAnimation()
        {
            _characterAudioController.OnLanding();
            SetTrigger(IntLanding);
        }
        private void WalkAnimation(bool isMove)
        {
            if (isMove)
            {
                SetFloat(IntMovement, 1);
            }
            else
            {
                SetFloat(IntMovement, 0);
            }

        }
        private void ChangeOnGroundState(bool onGround)
        {
            SetBool(IntOnGround, onGround);
        }
        private void ChangeStance(ActorStance stance)
        {
            SetInt(IntStance, stance.GetHashCode());
        }
        private void Jump()
        {
            SetTrigger(IntJump);
        }
        private void BlockStateChange(BlockState blockState)
        {
            if (blockState == BlockState.BlockEnable)
                SetBool(IntIsBlock, true);
            else if (blockState == BlockState.BlockDisable)
                SetBool(IntIsBlock, false);
        }
        private void OnAttack1()
        {
            SetTrigger(IntAttack1);                      
        }
        private void OnAttack2()
        {
            SetTrigger(IntAttack2);
        }
        private void SlashSound(int slashId)
        {
            _characterAudioController.SlashAudio(controller.GetSlashAudioClip(slashId));
        }
    }
}
