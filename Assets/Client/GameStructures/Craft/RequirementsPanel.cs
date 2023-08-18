using Assets.Client.GameStructures.Workshop;
using CustomTools;
using SpaceTraveler.GameStructures.Gear;
using SpaceTraveler.GameStructures.Items;
using SpaceTraveler.GameStructures.Characters.Player;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Craft.UI
{
    public class RequirementsPanel : MonoBehaviour
    {
        [SerializeField]
        private RequirementSlot _slotPrefab;
        [SerializeField]
        private Transform _slotsParent;

        private Pool<RequirementSlot> slotsPool;

        private IInteractingWithWorkshop player;
        public void Initialize(IInteractingWithWorkshop obj)
        {
            this.player = obj;
            slotsPool = new Pool<RequirementSlot>(_slotPrefab, 1, _slotsParent, true);
        }
        public void SetRequirements(CraftRequirements requirements)
        {
            slotsPool.HideObjects();


            if (requirements != null)
            {
                foreach (ElementSlot slot in requirements.Elements)
                {
                    int availableQuantity;

                    bool availability = CheckAvailability(slot.ItemID, slot.Amount, out availableQuantity);

                    string quantityStr = $"{availableQuantity}/{slot.Amount}";

                    var requirementSlot = slotsPool.GetFreeObject();

                    requirementSlot.SetSlot(slot.CurrentItem, quantityStr, availability);

                }
                foreach (Equipment equipment in requirements.Equipments)
                {
                    bool availability = CheckAvailability(equipment.Id);

                    var requirementSlot = slotsPool.GetFreeObject();

                    requirementSlot.SetSlot(equipment, availability);
                }

            }

        }
        public void Alert()
        {
            foreach(RequirementSlot slot in slotsPool.ActiveObjects)
            {
                if (!slot.Availability)
                    slot.Alert();
            }
        }
        private bool CheckAvailability(string id, int amount, out int availableQuantity)
        {
            var elementAmount = player.Inventory.GetItemAmount(id);

            availableQuantity = elementAmount;

            return elementAmount >= amount;
        }
        private bool CheckAvailability(string id)
        {
            return player.WorkshopSettings.HasEquipment(id);
        }

    }
}

