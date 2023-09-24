using SpaceTraveler.Audio;
using SpaceTraveler.GameStructures.Characters;
using SpaceTraveler.GameStructures.Characters.Player;
using UnityEngine;

namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public abstract class PlayerState : PlayerBaseState
    {
        protected bool onGround => player.Controller.OnGround;
        protected int currentStateHash;
        public PlayerState(Player player) : base(player)
        {
            this.player = player;
        }

        public override void Enter()
        {
            base.Enter();
            player.Controller.SurfaceCheckHandler.OnGroundStateChangeEvent += OnGroundStateChange;
            player.Controller.SurfaceCheckHandler.GroundTypeChangeEvent += OnGroundTypeChange;
            currentStateHash = SetStateNameHash();

            if(currentStateHash != 0) 
                player.AnimatorController.Play(currentStateHash);
        }
        public override void Exit()
        {
            player.Controller.SurfaceCheckHandler.OnGroundStateChangeEvent -= OnGroundStateChange;
            player.Controller.SurfaceCheckHandler.GroundTypeChangeEvent -= OnGroundTypeChange;
            base.Exit();
        }
        public virtual void UpdateLogick()
        {

        }
        public virtual void UpdatePhysics()
        {
        }

        protected virtual int SetStateNameHash()
        {
            if (string.IsNullOrEmpty(stateName))
            {
                return 0;
            }

            return Animator.StringToHash(stateMachine.GetMainStateName() + "." + stateName);
        }
        private void OnGroundTypeChange(GroundSettings settings)
        {
            if (isActive)
                player.AudioController.ChangeGroundSettings(settings.AudioSettings);
        }
        private void OnGroundStateChange(bool onGround)
        {
                if (!onGround)
                {
                    stateMachine.ChangeState(player.InAirState);
                }            
        }
    }
}
