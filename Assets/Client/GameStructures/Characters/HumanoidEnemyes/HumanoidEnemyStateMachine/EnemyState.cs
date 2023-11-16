using SpaceTraveler.Audio;
using SpaceTraveler.Characters.Player;
using SpaceTraveler.GameStructures.Characters.HumanoidEnemyes;
using UnityEngine;

namespace SpaceTraveler.Characters.HumanoidEnemyes.HumanoidEnemyStateMachine
{
    public class EnemyState : EnemyBaseState
    {
        protected bool onGround => enemy.Controller.OnGround;
        protected int currentStateHash;
        public EnemyState(HumanoidEnemy enemy) : base(enemy)
        {

        }
        public override void Enter()
        {
            base.Enter();
            controller.SurfaceCheckHandler.OnGroundStateChangeEvent += OnGroundStateChange;
            controller.SurfaceCheckHandler.GroundTypeChangeEvent += OnGroundTypeChange;
            currentStateHash = SetStateNameHash();

            if (currentStateHash != 0)
                animatorController.Play(currentStateHash);
        }
        public override void Exit()
        {
            controller.SurfaceCheckHandler.OnGroundStateChangeEvent -= OnGroundStateChange;
            controller.SurfaceCheckHandler.GroundTypeChangeEvent -= OnGroundTypeChange;
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
                enemy.AudioController.ChangeGroundSettings(settings.AudioSettings);
        }
        private void OnGroundStateChange(bool onGround)
        {

        }
    }
}
