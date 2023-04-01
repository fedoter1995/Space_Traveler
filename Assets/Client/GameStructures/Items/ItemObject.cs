using GameStructures.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField]
    protected ItemSlot _itemSlot;

    public ItemSlot ItemSlot { get => _itemSlot; set => _itemSlot = value; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var inventory = collision.GetComponent<Inventory>();


    }
}
