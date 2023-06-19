using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStructures.Spaceship
{
    public interface IStarshipInputManager
    {
        bool Move { get; }
        bool Fire { get; }
        int Rotation { get; }
    }
}
