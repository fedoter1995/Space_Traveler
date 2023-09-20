using SpaceTraveler.GameStructures.Characters.Actor.Data;
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
            playerController.Jump();
            isAbilityDone = true;
        }
    }
}