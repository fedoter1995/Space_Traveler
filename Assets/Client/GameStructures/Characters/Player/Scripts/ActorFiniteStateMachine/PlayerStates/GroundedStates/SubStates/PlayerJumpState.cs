using UnityEngine;

namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public class PlayerJumpState : PlayerAbilityState
    {
        protected int jumpInt = Animator.StringToHash("Jump");
        protected int jumpStateInt = Animator.StringToHash("JumpState");
        private bool isAbilityDone;

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