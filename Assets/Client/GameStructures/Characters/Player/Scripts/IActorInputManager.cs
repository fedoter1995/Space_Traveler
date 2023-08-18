using UnityEngine;

namespace SpaceTraveler.GameStructures.Characters.Player
{
    public interface IActorInputManager
    {
        bool IsMove { get; }
        bool IsRun { get; }
        Vector2 MoveVector { get; }
        public bool Jump { get; }

        public KeyCode AttackButton1 { get; }
        public KeyCode AttackButton2 { get; }
        public KeyCode ChangeStanceButton { get; }
        public KeyCode BlockButton { get; }
    }
}
