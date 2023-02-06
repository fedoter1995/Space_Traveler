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
        return _items.Find(item => item.Id == id);
    }

}
