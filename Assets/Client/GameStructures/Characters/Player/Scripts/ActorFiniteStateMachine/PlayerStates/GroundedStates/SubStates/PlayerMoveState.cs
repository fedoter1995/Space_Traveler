using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerMoveState : PlayerGroundedState
    {
        protected int moveInt = Animator.StringToHash("Move");
        public PlayerMoveState(Player player) : base(player)
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
            base.Exit();
            player.AnimatorController.SetBool(moveInt, false);
        }

        public override void UpdateLogick()
        {
            base.UpdateLogick();

            if(player.Controller.MoveX == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            player.Controller.Flip();
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            player.Controller.Move();
        }
    }
}

