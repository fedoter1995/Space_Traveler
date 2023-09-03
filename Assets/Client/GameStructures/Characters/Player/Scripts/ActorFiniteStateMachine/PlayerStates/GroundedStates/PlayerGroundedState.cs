using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public abstract class PlayerGroundedState : PlayerState
    {
        protected int groundedInHash = Animator.StringToHash("OnGround");
        protected PlayerGroundedState(Player player) : base(player)
        {
            player.Controller.InputHandler.JumpEvent += Jump;
            player.Controller.InputHandler.ChangeStanceEvent += ChangeStance;
        }

        private void Jump()
        {
           if(isActive)
            stateMachine.ChangeState(player.JumpState);
        }
        public override void UpdateLogick()
        {
            base.UpdateLogick();
        }
        private void ChangeStance()
        {
            if(isActive)
            {
                if (stateMachine.CurrentSuperState == player.ArmedState)
                    stateMachine.ChangeSuperState(player.UnarmedState);
                else
                    stateMachine.ChangeSuperState(player.ArmedState);
            }

        }
    }
}

