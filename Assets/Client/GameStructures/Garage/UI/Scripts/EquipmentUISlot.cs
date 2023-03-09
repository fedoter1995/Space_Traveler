using Architecture;
using GameStructures.Equipment;
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
    private Image _image;
    [SerializeField]
    private Image _stroke;


    private TooltipUI tooltip;

    public EquipmentBranch Branch { get; private set; }

    public Equipment Equip => _equip;

    public void Initialize(EquipmentBranch branch)
    {
        Branch = branch;
        tooltip = Game.GetUIController<TooltipUIController>().tooltip;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Branch.Three.SlotActual != this)
            Branch.Three.ChangeActiveEquip(this);

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
