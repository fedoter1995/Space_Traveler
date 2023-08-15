namespace SpaceTraveler.GameStructures.Hits
{
    public interface IDoingDamage : IDoingHit
    {
        void DealDamage(ITakeDamage target);
    }
}


