using SpaceTraveler.GameStructures.Gear;
using SpaceTraveler.GameStructures.ItemCollections;
using SpaceTraveler.GameStructures.Workshop;

namespace Assets.Client.GameStructures.Workshop
{
    public interface IInteractingWithWorkshop : IHaveEquipment, IHaveInventory, IHaveWorkshopSettings
    {

    }
}
