using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(Player player) : base(player)
        {
            stateName = "Move";
        }

        public override void Enter()
        {
            base.Enter();
            player.AnimatorController.Play(currentStateHash);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void UpdateLogick()
        {
            base.UpdateLogick();

            if(moveX == 0)           
                stateMachine.ChangeState(player.IdleState);         
            else
            {
                player.Controller.Move();

                if (moveX != playerController.Dirrection)
                    player.Controller.Flip();

            }   

        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            player.Controller.Move();
        }
    }
}

