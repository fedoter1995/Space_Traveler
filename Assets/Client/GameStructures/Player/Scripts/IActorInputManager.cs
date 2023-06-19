using System;
using UnityEngine;

namespace GameStructures.Player
{
    public interface IActorInputManager
    {
        bool IsMove { get; }
        bool IsRun { get; }
        Vector2 MoveVector { get; }
        public bool Jump { get; }
        public bool Attack1 { get; }
        public bool Attack2 { get; }
        public bool Attack3 { get; }
    }
}
