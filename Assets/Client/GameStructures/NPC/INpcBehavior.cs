
using System.Collections;

namespace GameStructures.NPC
{
    public interface INpcBehavior
    {
        IEnumerator ActionRoutine();
    }
}
