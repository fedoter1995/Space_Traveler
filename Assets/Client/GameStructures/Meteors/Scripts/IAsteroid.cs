using NavMesh;
namespace GameStructures.Meteors
{
    public interface IAsteroid : IDoingHit, IHaveNavMeshModifier, IPoolsObject<Asteroid>, IHaveTakeHitHandler
    {

    }
}
