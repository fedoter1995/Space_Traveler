using SpaceTraveler.Characters.Actor.ActorFiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerLandingState : PlayerGroundedState
    {
        public PlayerLandingState(Player player) : base(player)
        {
            stateName = "Landing";
            player.AnimatorController.EventsHandler.EndLandingEvent += EndLanding;
        }
        public override void Enter()
        {
            base.Enter();
            
            if (playerController.CurrentVelocityY < -10)
            {
                animatorController.Play(currentStateHash);
            }
            else
            {
                ChangeState();
            }

        }
        private void EndLanding()
        {
            ChangeState();
        }
        private void ChangeState()
        {
            if (moveX != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }

    }
}

