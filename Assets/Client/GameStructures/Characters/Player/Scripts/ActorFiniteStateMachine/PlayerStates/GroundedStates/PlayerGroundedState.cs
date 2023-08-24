using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public abstract class PlayerGroundedState : PlayerState
    {
        protected int groundedInHash = Animator.StringToHash("OnGround");
        protected int moveX;
        protected PlayerGroundedState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }



        public override void UpdateLogick()
        {
            base.UpdateLogick();
            moveX = player.InputHandler.MoveX;
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }
    }
}

