using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavMesh
{
    public interface IHaveNavMeshModifier
    {
        event Action OnDisableEvent;
        event Action OnEnableEvent;
    }
}
