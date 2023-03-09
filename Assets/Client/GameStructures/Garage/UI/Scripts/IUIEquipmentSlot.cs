using GameStructures.Equipment;
using UnityEngine.EventSystems;

public interface IUIEquipmentSlot : IPointerClickHandler
{
    public Equipment Equip { get; }
}

