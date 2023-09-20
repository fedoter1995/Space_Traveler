using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerIdleState : PlayerGroundedState
    {

        public PlayerIdleState(Player player) : base(player)
        {
            stateName = "Idle";
        }

        public override void Enter()
        {
            base.Enter();
            player.Controller.SetVelocityZero();
            player.AnimatorController.Play(currentStateHash);
        }

        public override void UpdateLogick()
        {
            base.UpdateLogick();
            if(moveX != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }
    }
}

