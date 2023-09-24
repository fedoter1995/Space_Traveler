namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public class PlayerLandingState : PlayerGroundedState
    {
        public PlayerLandingState(Player player) : base(player)
        {
            stateName = "Landing";
            player.AnimatorController.EventsHandler.EndLandingEvent += EndLanding;
        }
        public override void Enter()
        {
            base.Enter();
            
            if (playerController.CurrentVelocityY < -10)
            {
                playerAnimatorController.Play(currentStateHash);
                player.AudioController.OnLanding();
            }
            else
            {
                ChangeState();
            }

        }
        private void EndLanding()
        {
            ChangeState();
        }
        private void ChangeState()
        {
            if (moveX != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }

    }
}

