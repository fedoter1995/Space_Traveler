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
            if (!onGround)
            {
                stateMachine.ChangeState(player.InAirState);
            }

        }
    }
}