using SpaceTraveler.GameStructures.Stats;
using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Characters.Player
{
    [RequireComponent(typeof(AttackAnimationTriggerHandler))]
    public class ActorAnimatorController : AnimatorController
    {   
        private AttackAnimationTriggerHandler triggerHandler;

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
            triggerHandler = GetComponent<AttackAnimationTriggerHandler>();



            controller.OnGroundStateChangeEvent += ChangeOnGroundState;
            controller.OnJumpEvent += Jump;
            controller.OnLandingEvent += TriggerLandingAnimation;
            controller.OnChangeStanceEvent += ChangeStance;
            controller.OnAttack1Event += OnAttack1;
            controller.OnAttack2Event += OnAttack2;
            controller.OnBlockStateChangeEvent += BlockStateChange;
            controller.OnMoveStateChangeEvent += WalkAnimation;
            triggerHandler.OnAttackTriggerEvent += controller.OnAttackTriggered;
        }
        public void TakeDamageAnimation(DamageAttributes stats)
        {
            SetTrigger(IntHurt);
        }
        private void TriggerLandingAnimation()
        {        
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

    }
}
