using UnityEngine;

namespace SpaceTraveler.GameStructures.Items
{
    [System.Serializable]
    public class LootSlot : ItemSlot
    {
        [SerializeField, Range(1f, 100f)]
        private float _chnceToDrop = 100f;
        public float DropChance => _chnceToDrop;
    }
}

