using Architecture;
using GameStructures.Garage.Workshop;
using GameStructures.Gear;
using System;
using System.Collections;
using System.Collections.Generic;
using UI.Tooltip;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
public class EquipmentUISlot : TooltipUIObject, IUIEquipmentSlot
{
    [SerializeField]
    private Equipment _equip;
    [SerializeField]
    private CraftRequirements _requirements;

    [SerializeField]
    private Image _image;
    [SerializeField]
    private Image _stroke;
    [SerializeField]
    private Image _hideFilter;

    public bool Availability { get; private set; }

    public event Action<EquipmentUISlot> OnClickEvent;

    private TooltipUI tooltip;

    public EquipmentBranch Branch { get; private set; }
    public CraftRequirements Requirements => _requirements;
    public Equipment Equip => _equip;

    public void Initialize(EquipmentBranch branch)
    {
        Branch = branch;
        tooltip = Game.GetUIController<TooltipUIController>().tooltip;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(this);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        ShowDescription(eventData);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }


    public void SetActive(bool activity)
    {
        _stroke.gameObject.SetActive(activity);
    }
    public void SetAvailability(bool availability)
    {
        _hideFilter.gameObject.SetActive(!availability);
        Availability = availability;
    }

    private void ShowDescription(PointerEventData eventData)
    {
        var description = Equip.GetDescriptionData();
        tooltip.ShowTooltip(description, eventData);
    }

    void OnGUI()
    {
        if (_image != null && _equip != null)
            if (_image.sprite != _equip.Icon)
                _image.sprite = _equip.Icon;
    }

}
