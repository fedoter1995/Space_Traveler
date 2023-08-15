using System.Collections.Generic;

namespace SpaceTraveler.GameStructures.Gear
{
    public interface IEqupmentModuleSet<T> where T : Equipment 
    {
        public List<T> GetEquipment();
        public bool TrySetEquipment(T equipment);
        public bool TrySetEquipment(string id);

    }
}
