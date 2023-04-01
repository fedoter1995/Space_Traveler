using GameStructures.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LootSlot : ItemSlot
{
    [SerializeField, Range(1f, 100f)]
    private float _chnceToDrop = 100f;
    public float DropChance => _chnceToDrop;
}
