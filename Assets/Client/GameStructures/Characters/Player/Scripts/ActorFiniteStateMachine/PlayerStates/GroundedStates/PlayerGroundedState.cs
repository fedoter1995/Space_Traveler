using SpaceTraveler.Audio;
using System;
using UnityEngine;

namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public abstract class PlayerGroundedState : PlayerState
    {
        protected int moveX => player.Controller.MoveX;
        protected PlayerGroundedState(Player player) : base(player) { }
        public override void Enter()
        {
            base.Enter();
            player.InputHandler.JumpEvent += Jump;
            player.InputHandler.ChangeStanceEvent += ChangeStance;
            player.InputHandler.FirstAttackEvent += OnFirstAttackTriggered;
            player.OnLedgeState.CanGrab = true;
        }

        public override void Exit()
        {
            player.InputHandler.JumpEvent -= Jump;
            player.InputHandler.ChangeStanceEvent -= ChangeStance;
            player.Controller.InputHandler.FirstAttackEvent -= OnFirstAttackTriggered;
            base.Exit();
        }
        public override void UpdateLogick()
        {
            base.UpdateLogick();
        }
        private void Jump()
        {
            if (isActive)
            {
                stateMachine.ChangeState(player.JumpState);
            }
        }
        private void ChangeStance()
        {
            if(isActive && onGround)
            {
                if (stateMachine.CurrentArmamentState == player.ArmedState)
                    stateMachine.ChangeArmamentState(player.UnarmedState);
                else
                    stateMachine.ChangeArmamentState(player.ArmedState);
            }

        }
        private void OnFirstAttackTriggered()
        {
            if (stateMachine.CurrentArmamentState != player.ArmedState)
                stateMachine.ChangeArmamentState(player.ArmedState);

            stateMachine.ChangeState(player.FirstAttackState);
        }

    }
}

