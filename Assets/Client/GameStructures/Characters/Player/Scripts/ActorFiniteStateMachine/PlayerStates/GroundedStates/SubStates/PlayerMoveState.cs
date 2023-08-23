using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerMoveState : PlayerGroundedState
    {
        protected int moveInt = Animator.StringToHash("Move");
        public PlayerMoveState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();
            player.AnimatorController.SetBool(moveInt, true);
        }

        public override void Exit()
        {
            player.AnimatorController.SetBool(moveInt, false);
        }

        public override void UpdateLogick()
        {
            base.UpdateLogick();

            if(moveVector.x == 0)
            {
                stateMachine.ChangeState(player.UnarmedIdleState);
            }
            player.Controller.Move(moveVector.x);
        }

        public override void UpdatePhysics()
        {
        }
    }
}

