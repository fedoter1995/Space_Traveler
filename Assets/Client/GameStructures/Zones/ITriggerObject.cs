using UnityEngine;

namespace GameStructures.Zones
{
    public interface ITriggerObject
    {
        TriggerObjectType Type { get; }
        Vector3 Position { get; }
    }
}
