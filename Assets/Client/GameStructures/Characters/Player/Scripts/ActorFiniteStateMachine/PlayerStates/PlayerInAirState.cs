using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerInAirState : PlayerState
    {
        private int yVelocityHash = Animator.StringToHash("YVelocity");
        private int dirrection => playerController.Dirrection;


        public PlayerInAirState(Player player) : base(player)
        {
            stateName = "In_Air";
        } 

        public override void Enter()
        {
            base.Enter();
            animatorController.Play(currentStateHash);
        }
        public override void UpdateLogick()
        {
            float yVelocity = playerController.CurrentVelocityY;

            if (onGround && yVelocity < 0.01f)
            {
                stateMachine.ChangeState(player.LandingState);
            }

            animatorController.SetFloat(yVelocityHash, yVelocity);

            if(surfaceCheckHandler.CheckLedge(dirrection))
            {
                stateMachine.ChangeState(player.LadgeClimbState);
            }
        }
    }
}



