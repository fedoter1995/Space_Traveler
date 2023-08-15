using SpaceTraveler.GameStructures.Stats.StatModifiers;

namespace SpaceTraveler.GameStructures.Stats
{
    public interface IStatModifier
    {
        StatModType Type { get; }
        float Value { get; }
    }
}

