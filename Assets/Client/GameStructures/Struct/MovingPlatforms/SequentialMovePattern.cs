using UnityEngine;

namespace GameStructures.MovingPlatforms
{
    [CreateAssetMenu(menuName = "MovePatterns/Platforms/SequentialMovePattern")]
    public class SequentialMovePattern : PlatformMovePattern
    {
        public override int GetMoveIndex(int currentPositionIndex)
        {

            if (currentPositionIndex + 1 > positionsCount - 1)
                return 0;
            else 
                return currentPositionIndex + 1;

        }
    }
}
