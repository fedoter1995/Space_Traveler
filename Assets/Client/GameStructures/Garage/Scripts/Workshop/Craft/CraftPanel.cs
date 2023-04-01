using GameStructures.Garage.Workshop;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class CraftPanel : MonoBehaviour
{
    [SerializeField]
    private RequirementsPanel _requirements;
    [SerializeField]
    private EquipmentDescription _descriptionPanel;
    [SerializeField]
    private DubleStateButton _changeButton;




    private CraftHandler craftHandler;

    public Button.ButtonClickedEvent ButtonClickEvent => _changeButton.onClick;


    public void Initialize()
    {
        craftHandler = new CraftHandler();

        _changeButton.Initialize();
        _requirements.Initialize();


    }

    public void OnChangeObject(EquipmentUISlot slot)
    {

        _changeButton.ChangeState(slot.Availability);

        if (slot.Availability)
            _requirements.SetRequirements(null);
        else
            _requirements.SetRequirements(slot.Equip.Requirements);

        _descriptionPanel.Show(slot.Equip.GetDescriptionData());
    }
    public bool TryToCraftEquipment(EquipmentUISlot slot)
    {
        if (craftHandler.TryToCraftEquipment(slot.Equip))
        {
            slot.SetAvailability(true);
            return true;
        }
        return false;
    }

}
