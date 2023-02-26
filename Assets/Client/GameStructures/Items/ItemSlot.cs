using System;
using UnityEngine;
using System.Runtime.Serialization;
using System.Collections.Generic;

[System.Serializable]
public class ItemSlot : IJsonSerializable
{ 
    [SerializeField]
    protected Item _item;

    internal void Initialize()
    {
        throw new NotImplementedException();
    }

    [SerializeField]
    protected int _amount = 0;
    public Item CurrentItem => _item;
    public bool IsEmpty => _item == null;
    public string ItemID => _item == null ? "Empty_Slot" : _item.Id;
    public int Amount { get => IsEmpty ? 0 : _amount; set => _amount = value; }




    public void SetItem(Item item)
    {
        if (item == null)
        {
            Clear();
            return;
        }

        _item = item;

    }
    public void Clear()
    {
        _amount = 0;
        _item = null;
    }

    public void SetObjectData(Dictionary<string, object> obj)
    {
        if (obj != null)
        {
            var repository = Architecture.Game.GetRepository<ItemsRepository>();
            var id = obj["ID"].ToString();
            var amount = System.Convert.ToInt32(obj["Amount"]);
            SetItem(repository.GetItem(id));
            _amount = amount;
        }
    }

    public Dictionary<string, object> GetObjectData()
    {
        var dict = new Dictionary<string, object>();

        if (!IsEmpty)
        {
            dict.Add("ID", ItemID);
            dict.Add("Amount", Amount);
        }
        return dict;
    }
}