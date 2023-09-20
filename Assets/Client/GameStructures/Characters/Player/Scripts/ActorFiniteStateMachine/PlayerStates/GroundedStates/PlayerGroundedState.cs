using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public abstract class PlayerGroundedState : PlayerState
    {
        protected int moveX => player.Controller.MoveX;

        protected PlayerGroundedState(Player player) : base(player)
        {
            player.Controller.InputHandler.JumpEvent += Jump;
            player.Controller.InputHandler.ChangeStanceEvent += ChangeStance;
            player.Controller.SurfaceCheckHandler.OnGroundStateChangeEvent += OnGroundStateChange;
        }
        public override void Enter()
        {
            base.Enter();
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
        private void OnGroundStateChange(bool onGround)
        {
            if (isActive)
            {
                if (!onGround)
                {
                    stateMachine.ChangeState(player.InAirState);
                }
            }
        }

        
    }
}

