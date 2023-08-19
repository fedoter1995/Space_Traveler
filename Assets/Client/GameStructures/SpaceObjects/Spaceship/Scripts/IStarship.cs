using SpaceTraveler.GameStructures.Characters.Player;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Zones;

namespace SpaceTraveler.GameStructures.Spaceship
{
    public interface IStarship : IJsonSerializable, ITakeDamage, IHaveStatsHandler<StarshipStatsHandler>, IPlayerObject
    {

    }
}
