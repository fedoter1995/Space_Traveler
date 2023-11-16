
using SpaceTraveler.Characters.Player;
using SpaceTraveler.GameStructures.Characters.HumanoidEnemyes;

namespace SpaceTraveler.Characters.HumanoidEnemyes.HumanoidEnemyStateMachine
{
    public class EnemyIdleState : EnemyGroundedState
    {
        public EnemyIdleState(HumanoidEnemy enemy) : base(enemy)
        {
            stateName = "Idle";
        }
        public override void UpdateLogick()
        {
            base.UpdateLogick();
        }
    }
}
