using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LootSlot : ItemSlot
{
    [SerializeField, Range(0.001f, 1f)]
    private float _chnceToDrop = 1f;

    public float DropChance => _chnceToDrop;
}
