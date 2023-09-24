namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public class PlayerGrabState : PlayerState
    {
        public PlayerGrabState(Player player) : base(player)
        {
            stateName = "Grab";
        }

        public override void Enter()
        {
            base.Enter();
            player.transform.position = player.OnLedgeState.StartPosition;
            playerAnimatorController.Play(currentStateHash);
        }
        public override void UpdateLogick()
        {
            base.UpdateLogick();

            if (player.InputHandler.MoveY > 0)
                stateMachine.ChangeState(player.LedgeClimbState);

            if (player.InputHandler.MoveY < 0)
            {
                stateMachine.ClearSuperState();
                stateMachine.ChangeState(player.InAirState);
            }

        }
    }
}
