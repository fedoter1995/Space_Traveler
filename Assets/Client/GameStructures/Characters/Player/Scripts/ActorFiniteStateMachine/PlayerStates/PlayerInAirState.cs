using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerInAirState : PlayerState
    {
        protected int fallInt = Animator.StringToHash("Fall");
        public PlayerInAirState(Player player) : base(player)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            animatorController.SetBool(fallInt, true);
        }
        public override void Exit()
        {
            base.Exit();
            animatorController.SetBool(fallInt, false);
        }
        public override void UpdateLogick()
        {
            base.UpdateLogick();


            if (playerController.OnGround)
            {
                if (playerController.MoveX != 0)
                    stateMachine.ChangeState(player.MoveState);
                else
                    stateMachine.ChangeState(player.LandingState);
            }
            else if (playerController.SurfaceCheckHandler.CheckLedge(playerController.Dirrection))
            {
                stateMachine.ChangeState(player.LadgeClimbState);
            }
            player.Controller.Flip();

        }
    }
}



