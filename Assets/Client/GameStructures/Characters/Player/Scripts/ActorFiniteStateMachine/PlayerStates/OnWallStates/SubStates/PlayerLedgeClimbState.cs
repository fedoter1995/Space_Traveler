using DG.Tweening;
using JetBrains.Annotations;

namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public class PlayerLedgeClimbState : PlayerState
    {
        public PlayerLedgeClimbState(Player player) : base(player)
        {
            stateName = "Climb";
        }

        public override void Enter()
        {
            base.Enter();
            playerAnimatorController.Play(currentStateHash);
            playerAnimatorController.EventsHandler.EndClimbEvent += OnEndClimb;
        }
        public override void Exit()
        {
            base.Exit();
            stateMachine.ClearSuperState();
            playerAnimatorController.EventsHandler.EndClimbEvent -= OnEndClimb;
        }
        
        private void OnEndClimb()
        {
            player.transform.DOMove(player.OnLedgeState.EndPosition, 0.1f);

            if (player.InputHandler.MoveX != 0)            
                stateMachine.ChangeState(player.MoveState);
            else
                stateMachine.ChangeState(player.IdleState);
            
        }

    }
}
