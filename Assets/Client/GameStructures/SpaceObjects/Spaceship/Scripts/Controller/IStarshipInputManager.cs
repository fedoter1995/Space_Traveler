namespace GameStructures.Spaceship
{
    public interface IStarshipInputManager
    {
        bool Move { get; }
        bool Fire { get; }
        int Rotation { get; }
    }
}
