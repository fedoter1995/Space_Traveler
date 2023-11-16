using System;
using UnityEngine;

namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public class PlayerAttackState : PlayerAbilityState
    {
        PlayerAttackTransition transition;
        public PlayerAttackState(Player player, string attackStateName) : base(player) 
        {
            stateName = attackStateName;
        }
        public override void Enter()
        {
            base.Enter();
            playerAnimatorController.EventsHandler.BeginAttackEvent += OnBeginAttack;
            playerAnimatorController.EventsHandler.EndAttackEvent += OnAttackEnd;
            playerAnimatorController.EventsHandler.DamageTriggeredEvent += OnDamageTriggered;
            playerAnimatorController.Play(currentStateHash);
            playerController.SetVelocityZero();
        }

        public override void Exit()
        {
            base.Exit();
            playerAnimatorController.EventsHandler.EndAttackEvent -= OnAttackEnd;
            playerAnimatorController.EventsHandler.DamageTriggeredEvent -= OnDamageTriggered;
        }
        public void SetTransition(PlayerAttackTransition transition)
        {
            this.transition = transition;
        }
        private void OnBeginAttack(int id)
        {
           var audioclip = playerController.CombatController.GetAudioClip(id);
           player.AudioController.SlashAudio(audioclip);
        }
        private void OnAttackEnd(int attackId)
        {
            if(transition != null)
            {
                stateMachine.ChangeState(transition);
            }
            else
            {
                if (player.InputHandler.MoveX == 0)
                    stateMachine.ChangeState(player.IdleState);
                else
                    stateMachine.ChangeState(player.MoveState);
            }

        }
        private void OnDamageTriggered(int attackId)
        {
            player.Controller.TriggeredAttack(attackId);

        }

    }
}
