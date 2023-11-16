using UnityEngine;


namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public class PlayerBlockState : PlayerGroundedState
    {
        public PlayerBlockState(Player player) : base(player)
        {
            stateName = "Block";
        }
        protected override void ShieldBlock(bool isBlock)
        {
            if (!isBlock)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
        public override void Enter()
        {
            base.Enter();
            playerController.SetVelocityZero();
        }
    }
}
