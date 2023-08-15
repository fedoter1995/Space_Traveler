using UnityEngine;

namespace SpaceTraveler.GameStructures.Zones
{
    public interface ITriggerObject
    {
        TriggerObjectType Type { get; }
        Vector3 Position { get; }
    }
}
