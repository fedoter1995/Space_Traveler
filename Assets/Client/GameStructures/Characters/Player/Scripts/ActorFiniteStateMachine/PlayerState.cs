using Assets.Client.Characters.Player;
using SpaceTraveler.GameStructures.Characters;
using SpaceTraveler.GameStructures.Characters.Player;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public abstract class PlayerState
    {
        protected string stateName;
        public string Name => stateName;

        protected Player player;

        protected bool isActive = false;
        protected float startTime;
        protected int currentStateHash;

        protected bool onGround => player.Controller.OnGround;
        protected PlayerStateMachine stateMachine => player.StateMachine;
        protected ActorStatsHandler actorStatsHandler => player.StatsHandler;
        protected PlayerAnimatorController animatorController => player.AnimatorController;
        protected PlayerController playerController => player.Controller;
        protected CharacterSurfaceCheckHandler surfaceCheckHandler => playerController.SurfaceCheckHandler;
        public PlayerState(Player player)
        {
            this.player = player;
        }

        public virtual void Enter()
        {
            Debug.Log($"Enter in {this}");
            isActive = true;
            currentStateHash = SetStateNameHansh();
            startTime = Time.time;
        }

        public virtual void Exit()
        {
            isActive = false;
        }

        public virtual void UpdateLogick()
        {

        }
        public virtual void UpdatePhysics()
        {
        }
        private int SetStateNameHansh()
        {
            return Animator.StringToHash(stateMachine.GetMainStateName() + "." + stateName);
        }
    }
}
