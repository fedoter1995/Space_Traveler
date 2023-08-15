using System;

namespace NavMesh
{
    public interface IHaveNavMeshModifier
    {
        event Action OnDisableEvent;
        event Action OnEnableEvent;
    }
}
