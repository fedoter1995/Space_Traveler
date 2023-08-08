using System;
using UnityEngine;

namespace GameStructures.MovingPlatforms
{
    [Serializable]
    public abstract class PlatformMovePattern : ScriptableObject, IPlatformMovePattern
    {
        protected int positionsCount;
        public void InitMovePattern(int positionsCount)
        {
            this.positionsCount = positionsCount;
        }
        public abstract int GetMoveIndex(int currentPosition);
    }
}
