using System.Collections.Generic;
using UnityEngine;

namespace CustomTools
{
    public class LootRandomizer
    {
        public static void DropLoot(List<LootSlot> slots, Vector3 position)
        {
            var loot = GiveLoot(slots);
            if(loot.Count > 0)
                foreach (ItemSlot slot in loot)
                {
                    var item = Object.Instantiate(slot.CurrentItem.Prefab, position, Quaternion.identity);
                    item.ItemSlot = slot;
                    item.ItemSlot.CurrentItem.Initialize(item.ItemSlot.CurrentItem);
                }
        }
        private static List<ItemSlot> GiveLoot(List<LootSlot> slot)
        {
            var issuedSlots = new List<ItemSlot>();

            foreach(LootSlot itemSlot in slot)
            {
                if(itemSlot.CurrentItem == null)
                    continue;

                var randomNumb = Random.Range(0f, 1.0001f);


                if (itemSlot.DropChance >= randomNumb)
                {
                    issuedSlots.Add(itemSlot);
                }

            }

            return issuedSlots;
        }

    }

}

