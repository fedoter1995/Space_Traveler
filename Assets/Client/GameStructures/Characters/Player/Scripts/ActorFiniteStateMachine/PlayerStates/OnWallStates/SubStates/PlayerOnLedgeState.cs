using System;
using System.Collections;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerOnLedgeState : SuperState
    {
        
        protected float deltaTime = 1f;
        protected int climbInt = Animator.StringToHash("Climb");
        protected Vector2 cornerPosition;
        protected Vector2 startPosition;
        protected Vector2 endPosition;

        private bool isHanging;
        private bool isClimbing;

        public bool CanGrab { get; set; } = true;
        public PlayerOnLedgeState(Player player) : base(player)
        {
            stateName = "LedgeClimb";
            animatorController.EventsHandler.EndClimbEvent += EndClimb;
            animatorController.EventsHandler.EndGrabEvent += EndGrab;
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

                player.transform.position = startPosition;
            }
            CanGrab = false;
        }
        public override void Exit()
        {
            base.Exit();
            isClimbing = false;
            isHanging = false;

            player.Controller.SetBodyType(RigidbodyType2D.Dynamic);
        }
        public override void UpdateLogick()
        {
            base.UpdateLogick();

        }
        private void EndClimb()
        {
            isClimbing = false;
        }
        private void EndGrab()
        {
            isHanging = true;
        }
    }
}