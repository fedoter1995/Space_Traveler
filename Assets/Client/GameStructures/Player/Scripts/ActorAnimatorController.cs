using Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameStructures.Player
{
    public class ActorAnimatorController : AnimatorController
    {   
        private ActorController controller;

        #region Hash Animator Var
        private int IntSpeed = Animator.StringToHash("Speed");
        private int IntJump = Animator.StringToHash("Jump");
        private int IntIsAttack = Animator.StringToHash("IsAttack");
        private int IntAttack = Animator.StringToHash("Attack");
        #endregion

        public event Action OnAttackDealDamageEvent;

        public void Initialize(ActorController controller)
        {
            this.controller = controller;
            controller.OnJumpEvent += Jump;
            controller.OnLandingEvent += Landing;
            controller.OnBeginComboEvent += OnBeginCombo;
        }
        private void Update()
        {
            ChangeSpeed();
        }
        private void ChangeSpeed()
        {
            if (controller.IsRunning)
                SetFloat(IntSpeed, 1f);
            else if (controller.IsMoveing)
                SetFloat(IntSpeed, 0.5f);
            else
                SetFloat(IntSpeed, 0f);
        }
        private void Jump()
        {
            SetFloat(IntJump, 1f);
        }
        private void Landing()
        {
            SetFloat(IntJump, 0f);
        }
        private void OnBeginCombo()
        {
            SetBool(IntIsAttack, true);
            SetFloat(IntAttack, 0);                      
        }
        private void OnAttackEnd()
        {
            if (controller.AttackQueue.Count > 0)
            {
                controller.AttackQueue.Dequeue();
                if (controller.AttackQueue.Count > 0)
                {
                    SetBool(IntIsAttack, true);
                    SetFloat(IntAttack, controller.AttackQueue.Peek().AnimatorVarRef);
                }
                else
                {
                    SetBool(IntIsAttack, false);
                    controller.IsAttack = false;
                }
            }
            else
            {
                SetBool(IntIsAttack, false);
                controller.IsAttack = false;
            }
            
        }
        private void DealDamage()
        {
            controller.OnDealDamage();
        }
    }
}
