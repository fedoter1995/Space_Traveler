using Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameStructures.Player
{
    [RequireComponent(typeof(AttackAnimationTriggerHandler))]
    public class ActorAnimatorController : AnimatorController
    {   
        private ActorController controller;
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
        #endregion

        public event Action<int> AttackTriggerEvent;
        public void Initialize(ActorController controller)
        {
            this.controller = controller;
            triggerHandler = GetComponent<AttackAnimationTriggerHandler>();



            this.controller.OnGroundStateChangeEvent += ChangeOnGroundState;
            this.controller.OnJumpEvent += Jump;
            this.controller.OnLandingEvent += Landing;
            this.controller.OnChangeStanceEvent += ChangeStance;
            this.controller.OnAttack1Event += OnAttack1;
            this.controller.OnAttack2Event += OnAttack2;
            this.controller.OnBlockStateChangeEvent += BlockStateChange;
            this.controller.OnMoveStateChangeEvent += WalkAnim;
            triggerHandler.OnAttackTriggerEvent += OnAttackTriggerEvent;
        }

        private void Landing()
        {        
            SetTrigger(IntLanding);
        }
        private void OnAttackTriggerEvent(int attackId)
        {
            controller.OnAttackTriggered(attackId);
        }
        private void WalkAnim(bool isMove)
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
