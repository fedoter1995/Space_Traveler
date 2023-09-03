using UnityEditor;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public abstract class PlayerAbilityState : PlayerState
    {
        protected bool isAbilityDone;
        protected PlayerAbilityState(Player player) : base(player)
        {
        }

        public override void Enter()
        {
            base.Enter();
            isAbilityDone = false;
        }
        public override void UpdateLogick()
        {
            base.UpdateLogick();

            if (isAbilityDone)
            {
                if (playerController.OnGround && playerController.YVelocity < 0.01f)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
                else
                {
                    stateMachine.ChangeState(player.InAirState);
                }
            }
        }
    }
}