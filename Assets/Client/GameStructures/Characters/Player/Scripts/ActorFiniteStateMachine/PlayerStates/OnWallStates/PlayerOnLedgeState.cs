using System;
using System.Collections;
using UnityEngine;
namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public class PlayerOnLedgeState : SuperState
    {
        
        protected float deltaTime = 1f;

        protected int grabInt = Animator.StringToHash("Grab");
        protected int climbInt = Animator.StringToHash("Climb");


        protected Vector2 cornerPosition;
        protected Vector2 startPosition;
        protected Vector2 endPosition;


        public Vector2 StartPosition => startPosition;
        public Vector2 EndPosition => endPosition;
        public bool CanGrab { get; set; } = true;
        public PlayerOnLedgeState(Player player) : base(player)
        {
            stateName = "On_Ledge";
            playerAnimatorController.EventsHandler.EndClimbEvent += EndClimb;
            playerAnimatorController.EventsHandler.EndGrabEvent += EndGrab;
        }
        public override void Enter()
        {
            base.Enter();

            if (CanGrab)
            {
                player.Controller.SetBodyType(RigidbodyType2D.Static);
                cornerPosition = playerController.DetermineCornerPosition();

                startPosition.Set(cornerPosition.x - playerController.ClimbStartOffset.x, cornerPosition.y - playerController.ClimbStartOffset.y);
                endPosition.Set(cornerPosition.x + playerController.ClimbEndOffset.x, cornerPosition.y + playerController.ClimbEndOffset.y);

               

                stateMachine.ChangeState(player.GrabState);
            }



            CanGrab = false;
        }
        public override void Exit()
        {
            base.Exit();

            player.Controller.SetBodyType(RigidbodyType2D.Dynamic);
            playerController.SetVelocityZero();
        }
        private void EndClimb()
        {
            if(playerController.SurfaceCheckHandler.OnGround)
            {
                if (playerController.MoveX != 0)
                    stateMachine.ChangeState(player.MoveState);
                else
                    stateMachine.ChangeState(player.IdleState);
            }
        }
        private void EndGrab()
        {
            
        }
    }
}