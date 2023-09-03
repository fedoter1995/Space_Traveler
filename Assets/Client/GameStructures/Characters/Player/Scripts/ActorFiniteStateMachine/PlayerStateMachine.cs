

using System;
using UnityEngine;

namespace SpaceTraveler.Characters.Actor.ActorFiniteStateMachine
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; private set; }
        public SuperState CurrentSuperState { get; private set; }
        public void Initialize(SuperState initialSuperState, PlayerState initialState)
        {
            CurrentSuperState = initialSuperState;
            CurrentState = initialState;
            CurrentSuperState.Enter();
            CurrentState.Enter();
        }

        public void ChangeState(PlayerState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
        public void ChangeSuperState(SuperState newState)
        {
            CurrentSuperState.Exit();
            CurrentSuperState = newState;
            CurrentSuperState.Enter();
        }
    }
}
