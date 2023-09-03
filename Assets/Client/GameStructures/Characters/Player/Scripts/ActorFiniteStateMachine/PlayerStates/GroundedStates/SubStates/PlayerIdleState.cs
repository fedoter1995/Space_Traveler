using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerIdleState : PlayerGroundedState
    {
        protected int idleInt = Animator.StringToHash("Idle");
        public PlayerIdleState(Player player) : base(player)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();
            player.LadgeClimbState.CanGrab = true;
            player.AnimatorController.SetBool(idleInt, true);

        }

        public override void Exit()
        {
            player.AnimatorController.SetBool(idleInt, false);
        }

        public override void UpdateLogick()
        {
            base.UpdateLogick();
            if(player.Controller.MoveX != 0)
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

