using SpaceTraveler.GameStructures.Stats.StatModifiers;

namespace SpaceTraveler.GameStructures.Stats
{
    public interface IStatModifier
    {
        StatModifierType Type { get; }
        float Value { get; }
    }
}

