using System.Collections.Generic;

namespace Architecture
{
    public abstract class Repository : ArchitectureComponent, IRepository
    {
        public abstract Dictionary<string, object> GetObjectData();
        public abstract void SetObjectData(Dictionary<string, object> obj);
    }
}

