using Architecture;
using CustomTools;
using GameStructures.Garage.Workshop;
using GameStructures.Gear;
using GameStructures.Items;
using GameStructures.Spaceship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementsPanel : MonoBehaviour
{
    [SerializeField]
    private RequirementSlot _slotPrefab;
    [SerializeField]
    private Transform _slotsParent;

    private Pool<RequirementSlot> slotsPool;

    private Starship spaceship;
    public void Initialize()
    {
        spaceship = Game.GetInteractor<SpaceshipInteractor>().spaceship;
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
        var elementAmount = spaceship.Inventory.GetItemAmount(id);

        availableQuantity = elementAmount;

        return elementAmount >= amount;
    }
    private bool CheckAvailability(string id)
    {
        return spaceship.WorkshopSettings.HasEquipment(id);
    }

}
