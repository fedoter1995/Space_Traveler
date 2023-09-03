using SpaceTraveler.Characters.Actor.ActorFiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerLandingState : PlayerGroundedState
    {
        protected int landingInt = Animator.StringToHash("Landing");
        public PlayerLandingState(Player player) : base(player)
        {
            player.AnimatorController.EventsHandler.EndLandingEvent += EndLanding;
        }
        public override void Enter()
        {
            base.Enter();
            player.AnimatorController.SetBool(landingInt, true);
        }
        public override void Exit()
        {
            base.Exit();
            player.AnimatorController.SetBool(landingInt, false);
        }
        private void EndLanding()
        {
            stateMachine.ChangeState(player.IdleState);
        }

    }
}

