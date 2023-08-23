

using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; private set; }


        public void Initialize(PlayerState initialState)
        {
            CurrentState = initialState;
            Debug.Log(CurrentState);
            CurrentState.Enter();
        }
        public void ChangeState(PlayerState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
