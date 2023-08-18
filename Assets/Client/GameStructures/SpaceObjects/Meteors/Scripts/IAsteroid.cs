using NavMesh;
using SpaceTraveler.GameStructures.Hits;

namespace SpaceTraveler.GameStructures.Meteors
{
    public interface IAsteroid : IDoingHit, IHaveNavMeshModifier, IPoolsObject<Asteroid>, IHaveTakeHitHandler
    {

    }
}
