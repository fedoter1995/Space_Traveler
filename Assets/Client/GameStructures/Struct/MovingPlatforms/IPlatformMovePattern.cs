using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStructures.MovingPlatforms
{
    public interface IPlatformMovePattern
    {
        int GetMoveIndex(int currentPosition);
    }
}
