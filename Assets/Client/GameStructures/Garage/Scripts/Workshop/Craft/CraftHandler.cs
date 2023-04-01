using Architecture;
using GameStructures.Garage.Workshop;
using GameStructures.Gear;
using GameStructures.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftHandler
{
    private Spaceship spaceship;
    public CraftHandler()
    {
        spaceship = Game.GetInteractor<SpaceshipInteractor>().spaceship;
    }
    public bool TryToCraftEquipment(Equipment equipment)
    {
        if (CheckRequirements(equipment.Requirements))
        {           
            spaceship.WorkshopSettings.AddToAvailableEquipment(equipment);

            spaceship.Equipment.SetEquipment(equipment);

            return true;
        }
        return false;
    }
    private bool CheckRequirements(CraftRequirements requirements)
    {
        var inventory = spaceship.Inventory;

        var availableEquipment = spaceship.WorkshopSettings.AvailableEquipment;

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
