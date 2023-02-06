using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/New_Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private ItemObject _prefab;
    [SerializeField] private SlotType _slotType;
    public string Id => _id;
    public string Name => _name;
    public string Description => _description;
    public Sprite Icon => _icon;
    public ItemObject Prefab => _prefab;
    public SlotType SlotType => _slotType;


    public void Initialize(Item item)
    {
        _id = item.Id;
        _name = item.Name;
        _description = item.Description;
        _icon = item.Icon;
        _prefab = item.Prefab;
    }
}
