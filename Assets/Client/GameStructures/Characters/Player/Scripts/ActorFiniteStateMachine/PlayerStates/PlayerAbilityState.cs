using SpaceTraveler.Audio;
using UnityEditor;
using UnityEngine;

namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public abstract class PlayerAbilityState : PlayerState
    {
        protected PlayerAbilityState(Player player) : base(player)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }
        public override void Exit()
        {
            base.Exit();
        }

    }
}