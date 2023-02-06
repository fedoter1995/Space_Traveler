using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Inventory : ItemCollection
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.GetComponent<ItemObject>();
        if (obj != null)
            TryToAddToCollection(obj, obj.ItemSlot.CurrentItem, obj.ItemSlot.Amount);
    }
    public override string ToString()
    {
        return "Spaceship Inventory";
    }
}
