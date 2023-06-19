using GameStructures.Items;
using System;
using TMPro;
using UnityEngine;

public class RequirementSlot : MonoBehaviour,IPoolsObject<RequirementSlot>
{
    [SerializeField]
    private ItemUISlot _itemUISlot;
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    public Action<RequirementSlot> OnDisableObject { get; set; }

    public bool Availability { get; private set; } = false;

    
    public void SetSlot(Item item, string amount, bool availability)
    {
        _textMesh.gameObject.SetActive(true);
        _itemUISlot.SetItem(item, availability);
        _textMesh.text = amount;
        Availability = availability;
    }
    public void SetSlot(Item item, bool availability)
    {
        _itemUISlot.SetItem(item, availability);
        _textMesh.gameObject.SetActive(false);
        Availability = availability;
    }
    public void Alert()
    {
        _itemUISlot.Alert();
    }
    private void OnDisable()
    {
        OnDisableObject?.Invoke(this);
    }
}
