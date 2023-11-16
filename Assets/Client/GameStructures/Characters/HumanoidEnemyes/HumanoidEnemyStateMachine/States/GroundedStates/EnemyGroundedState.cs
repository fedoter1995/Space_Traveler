using SpaceTraveler.GameStructures.Characters.HumanoidEnemyes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTraveler.Characters.HumanoidEnemyes.HumanoidEnemyStateMachine
{
    public class EnemyGroundedState : EnemyState
    {
        public EnemyGroundedState(HumanoidEnemy enemy) : base(enemy)
        {
        }
    }
}
