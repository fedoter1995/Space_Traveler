using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerIdleState : PlayerGroundedState
    {
        protected int idleInt = Animator.StringToHash("Idle");
        public PlayerIdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();
            player.AnimatorController.SetBool(idleInt, true);

        }

        public override void Exit()
        {
            player.AnimatorController.SetBool(idleInt, false);
        }

        public override void UpdateLogick()
        {
            base.UpdateLogick();
            if(moveVector.x != 0)
            {
                stateMachine.ChangeState(player.UnarmedMoveState);
            }
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }
    }
}

