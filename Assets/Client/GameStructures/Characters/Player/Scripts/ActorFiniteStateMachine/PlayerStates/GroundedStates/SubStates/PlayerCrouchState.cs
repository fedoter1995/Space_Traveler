using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public class PlayerCrouchState : PlayerGroundedState
    {
        public PlayerCrouchState(Player player) : base(player)
        {
            stateName = "Crouch";
        }
        public override void Enter()
        {
            base.Enter();
            player.AnimatorController.EventsHandler.CrouchEndEvent += OnCrouchEnd;

        }
        public override void UpdateLogick()
        {
            base.UpdateLogick();
            if(player.InputHandler.MoveX != 0)
                stateMachine.ChangeState(player.MoveState);

        }
        private void OnCrouchEnd()
        {
            if(player.InputHandler.MoveX != 0)           
                stateMachine.ChangeState(player.MoveState);       
            else
                stateMachine.ChangeState(player.IdleState);
        }

    }
}
