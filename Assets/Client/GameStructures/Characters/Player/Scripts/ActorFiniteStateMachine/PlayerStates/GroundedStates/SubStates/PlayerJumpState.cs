using UnityEditor;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerJumpState : PlayerAbilityState
    {
        protected int jumpInt = Animator.StringToHash("Jump");
        protected int jumpStateInt = Animator.StringToHash("JumpState");
        public PlayerJumpState(Player player) : base(player)
        {
        }

        public override void Enter()
        {
            base.Enter();
            animatorController.SetBool(jumpInt, true);
            player.Controller.Jump(player.Controller.MoveX);
            isAbilityDone = true;
        }
        public override void Exit()
        {
            base.Exit();
            animatorController.SetBool(jumpInt, false);
        }

        public override void UpdateLogick()
        {
            base.UpdateLogick();
            Debug.Log(playerController.YVelocity);
            if (isAbilityDone)
            {
                if (playerController.YVelocity > 0)
                {
                    animatorController.SetFloat(jumpInt, 0);
                }
                else if(playerController.YVelocity < 0)
                {
                    animatorController.SetFloat(jumpInt, 0.5f);
                }
                else if(playerController.OnGround)
                    animatorController.SetFloat(jumpInt, 1f);

                if(playerController.OnGround)
                {
                    if (playerController.MoveX != 0)
                        stateMachine.ChangeState(player.MoveState);
                    else
                        stateMachine.ChangeState(player.IdleState);
                }
            }


        }

    }
}