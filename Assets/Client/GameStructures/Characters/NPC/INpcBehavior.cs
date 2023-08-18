
using System.Collections;

namespace SpaceTraveler.GameStructures.NPC
{
    public interface INpcBehavior
    {
        IEnumerator ActionRoutine();
    }
}
