using SpaceTraveler.Characters.Player.PlayerFiniteStateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTraveler.Characters.HumanoidEnemyes.HumanoidEnemyStateMachine
{
    public class EnemyStateMachine
    {
        public EnemyState CurrentState { get; private set; }
        public SuperState SuperState { get; private set; }

        public void Initialize(EnemyState initialState)
        {
            CurrentState = initialState;
            CurrentState.Enter();
        }
        public void ChangeState(EnemyState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
        public void SetSuperState(SuperState newState)
        {
            if (SuperState != null)
                ClearSuperState();

            SuperState = newState;
            SuperState.Enter();
        }
        public void ClearSuperState()
        {
            if (SuperState != null)
            {
                SuperState.Exit();
                SuperState = null;
            }
        }
        public string GetMainStateName()
        {
            if (SuperState != null)
                return SuperState.Name;
            else 
                return "";
        }
    }
}
