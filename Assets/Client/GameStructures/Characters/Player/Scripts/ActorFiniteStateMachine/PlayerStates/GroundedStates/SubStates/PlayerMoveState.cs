using UnityEngine;

namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(Player player) : base(player)
        {
            stateName = "Move";
        }

        public override void Enter()
        {
            base.Enter();
            playerAnimatorController.EventsHandler.StepEvent += OnStep;          
        }

        public override void Exit()
        {
            base.Exit();
            playerAnimatorController.EventsHandler.StepEvent -= OnStep;
        }

        public override void UpdateLogick()
        {
            base.UpdateLogick();

            if(moveX == 0)           
                stateMachine.ChangeState(player.IdleState);         
            else
            {
                player.Controller.Move();

                if (moveX != playerController.Dirrection)
                    player.Controller.Flip();

            }   

        }
        private void OnStep()
        {
            player.AudioController.OnStep();
        }
        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            player.Controller.Move();
        }
    }
}

