using GameStructures.Stats;
using GameStructures.Zones;

namespace GameStructures.Spaceship
{
    public interface IStarship : IJsonSerializable, ITriggerObject, IHaveStatsHandler<StarshipStatsHandler>, IHaveTakeHitHandler
    {

    }
}
