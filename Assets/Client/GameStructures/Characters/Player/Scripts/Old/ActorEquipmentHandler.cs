using SpaceTraveler.GameStructures.Gear;
using SpaceTraveler.GameStructures.Stats.StatModifiers;
using System.Collections.Generic;

namespace SpaceTraveler.GameStructures.Characters.Player
{
    public class ActorEquipmentHandler : IEqupmentHandler
    {
        public List<StatModifier> GetAllModifiers()
        {
            throw new System.NotImplementedException();
        }

        public List<Equipment> GetEquipment()
        {
            throw new System.NotImplementedException();
        }

        public bool TrySetEquipment(Equipment equipment)
        {
            throw new System.NotImplementedException();
        }

        public bool TrySetEquipment(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
