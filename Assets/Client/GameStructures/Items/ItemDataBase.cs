using GameStructures.Gear;
using GameStructures.Gear.Armors;
using GameStructures.Gear.Engine;
using GameStructures.Gear.Weapons;
using GameStructures.Items;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/DataBase")]
public class ItemDataBase : ScriptableObject 
{
    [SerializeField]
    private string _name;

    [SerializeField, Header("Weapons")]
    private List<Weapon> _weapons = new List<Weapon>();
    [SerializeField, Header("Armors")]
    private List<ShipSkin> _armors = new List<ShipSkin>();
    [SerializeField, Header("Engines")]
    private List<MainEngine> _engines = new List<MainEngine>();
    [SerializeField, Header("Elements")]
    private List<Element> _elements = new List<Element>();
    [SerializeField, Header("Other Items")]
    private List<Item> _items = new List<Item>();
    public string Name => _name;
    public T GetItem<T>(string id) where T : Item
    {
        T item = null;

        var itemType = typeof(T);

        if(typeof(T) == typeof(Equipment) || itemType.IsSubclassOf(typeof(Equipment)))
        {
            var items = new List<Equipment>(_weapons);

            items.AddRange(_armors);
            items.AddRange(_engines);

            item = (T)FindItem(items, id);
        }
        else if(typeof(T) == typeof(Element) || itemType.IsSubclassOf(typeof(Element)))
        {
            item = (T)FindItem(_elements, id);
        }
        else
            item = (T)FindItem(_items, id);

        return item;

    }
    private Item FindItem<T>(List<T> targetList, string id) where T : Item
    {
        var items = targetList.FindAll(stat => stat.Id == id);

        Item item = null;

        switch (items.Count)
        {
            case 1:
                {
                    item = items[0];
                    break;
                }
            case > 1:
                {
                    throw new System.Exception($"Found more than one item with id = {id}");
                }
            case 0:
                {
                    throw new System.Exception($"No item with id = {id} found");
                }
        }
        return item;
    }
}
