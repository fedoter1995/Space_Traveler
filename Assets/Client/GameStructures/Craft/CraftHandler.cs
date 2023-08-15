using Assets.Client.GameStructures.Workshop;
using SpaceTraveler.GameStructures.Gear;
using SpaceTraveler.GameStructures.Items;

namespace SpaceTraveler.GameStructures.Craft
{
    public class CraftHandler
    {
        private IInteractingWithWorkshop interactingObject;
        public CraftHandler(IInteractingWithWorkshop interactingObject)
        {
            this.interactingObject = interactingObject;
        }
        public bool TryToCraftEquipment(Equipment equipment)
        {
            if (CheckRequirements(equipment.Requirements))
            {
                interactingObject.WorkshopSettings.AddToAvailableEquipment(equipment);

                if(interactingObject.Equipment.TrySetEquipment(equipment))
                    return true;
            }
            return false;
        }
        private bool CheckRequirements(CraftRequirements requirements)
        {
            var inventory = interactingObject.Inventory;

            var availableEquipment = interactingObject.WorkshopSettings.AvailableEquipment;

            foreach (Equipment equipment in requirements.Equipments)
                if (!availableEquipment.Contains(equipment))
                    return false;


            foreach (ElementSlot slot in requirements.Elements)
                if (!inventory.HaveItemAmount(slot.CurrentItem, slot.Amount))
                    return false;

            foreach (ElementSlot slot in requirements.Elements)
                inventory.TryToRemove(slot.CurrentItem, slot.Amount);

            return true;
        }
    }
}

