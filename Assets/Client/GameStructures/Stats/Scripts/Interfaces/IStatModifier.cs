namespace GameStructures.Stats
{
    public interface IStatModifier
    {
        StatModType Type { get; }
        float Value { get; }
    }
}

