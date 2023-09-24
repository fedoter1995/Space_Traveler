using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public class PlayerAttackTransition : PlayerState
    {
        private PlayerAttackState nextState;
        public PlayerAttackTransition(Player player, string transitionName) : base(player)
        {
            stateName = transitionName;
        }
        public override void Enter()
        {
            base.Enter();
            playerAnimatorController.EventsHandler.EndTransitionEvent += OnTransitionEnd;
            player.InputHandler.FirstAttackEvent += OnAttackButtonPressed;
            playerAnimatorController.Play(currentStateHash);
        }
        public override void Exit()
        {
            base.Exit();
            playerAnimatorController.EventsHandler.EndTransitionEvent -= OnTransitionEnd;
            player.InputHandler.FirstAttackEvent -= OnAttackButtonPressed;

        }
        public void SetNextAttackState(PlayerAttackState state)
        {
            nextState = state;
        }
        private void OnAttackButtonPressed()
        {
            if(nextState != null)
            {
                stateMachine.ChangeState(nextState);
            }

        }
        private void OnTransitionEnd()
        {

            if (player.InputHandler.MoveX != 0)
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
