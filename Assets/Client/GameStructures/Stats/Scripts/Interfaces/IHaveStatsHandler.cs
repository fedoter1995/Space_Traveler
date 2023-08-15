
namespace SpaceTraveler.GameStructures.Stats
{
    public interface IHaveStatsHandler<T> where T : StatsHandler
    {
        public T StatsHandler { get; }
    }
}
