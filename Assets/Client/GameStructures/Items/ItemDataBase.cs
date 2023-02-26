using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/DataBase")]
public class ItemDataBase : ScriptableObject 
{
    [SerializeField]
    private string _name;

    [SerializeField, Header("Items")]
    private List<Item> _items = new List<Item>();

    public string Name => _name;
    public Item GetItem(string id)
    {
        var items = _items.FindAll(stat => stat.Id == id);
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
