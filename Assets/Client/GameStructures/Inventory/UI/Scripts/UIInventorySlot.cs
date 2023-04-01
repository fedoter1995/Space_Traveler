using GameStructures.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventorySlot : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem _uiItem;


    public void SetSlot(IItemSlot slot)
    {
        _uiItem.SetItemSlot(slot);
    }
}
