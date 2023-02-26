using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private TextMeshProUGUI _count;
    [SerializeField]
    private Image _icon;


    private ItemSlot itemSlot;


    public void SetItemSlot(ItemSlot slot)
    {
        itemSlot = slot;
        _name.text = itemSlot.CurrentItem.name;
        _count.text = itemSlot.Amount.ToString();
        _icon.sprite = itemSlot.CurrentItem.Icon;
    }
}
