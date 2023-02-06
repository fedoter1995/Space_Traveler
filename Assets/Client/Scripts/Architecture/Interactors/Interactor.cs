using System.Collections.Generic;

namespace Architecture
{
    public abstract class Interactor : ArchitectureComponent, IInteractor
    {
        public abstract Dictionary<string, object> GetObjectData();
        public abstract void SetObjectData(Dictionary<string, object> obj);

    }
}

