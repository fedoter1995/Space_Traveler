using SpaceTraveler.GameStructures.Stats.StatModifiers;
using System.Collections.Generic;

namespace SpaceTraveler.GameStructures.Gear
{
    public interface IEqupmentHandler
    {
        public List<StatModifier> GetAllModifiers();

        public List<Equipment> GetEquipment();
        public bool TrySetEquipment(Equipment equipment);
        public bool TrySetEquipment(string id);

    }
}
