using SpaceTraveler.GameStructures.Player;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Zones;

namespace SpaceTraveler.GameStructures.Spaceship
{
    public interface IStarship : IJsonSerializable, ITriggerObject, IHaveStatsHandler<StarshipStatsHandler>, IHaveTakeHitHandler, IPlayerObject
    {

    }
}
