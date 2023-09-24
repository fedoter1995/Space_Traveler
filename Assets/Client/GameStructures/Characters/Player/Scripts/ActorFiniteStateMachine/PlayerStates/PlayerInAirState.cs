using SpaceTraveler.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
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

            playerAnimatorController.Play(currentStateHash);
        }
        public override void Exit()
        {
            base.Exit();

        }
        public override void UpdateLogick()
        {
            float yVelocity = playerController.CurrentVelocityY;

            if (onGround && yVelocity < 0.01f)
            {
                stateMachine.ChangeState(player.LandingState);
            }

            playerAnimatorController.SetFloat(yVelocityHash, yVelocity);

            if(surfaceCheckHandler.CheckLedge(dirrection) && player.OnLedgeState.CanGrab)
            {
                stateMachine.SetSuperState(player.OnLedgeState);
            }
        }

    }
}



