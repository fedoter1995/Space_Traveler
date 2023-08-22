using SpaceTraveler.GameStructures.ItemCollections;
using SpaceTraveler.GameStructures.Items;
using System.Collections.Generic;
using UnityEngine;


namespace CustomTools
{
    public class LootRandomizer : MonoBehaviour
    {
        [SerializeField]
        protected List<LootSlot> _loot;

        public void DropLoot(object sender, Inventory target)
        {
            var droppedItems = GiveLoot(_loot);
            var notIncludedSlots = new List<ItemSlot>();
            target.TryToAddToCollection(sender, droppedItems,out notIncludedSlots);
        }
        private  List<ItemSlot> GiveLoot(List<LootSlot> slots)
        {
            var issuedSlots = new List<ItemSlot>();

            System.Random rnd = new System.Random();

            foreach (LootSlot slot in slots)
            {
                float numb = rnd.Next(1, 101);

                if (slot.DropChance >= numb)
                {
                    var newSlot = new ItemSlot();
                    newSlot.SetItem(slot.CurrentItem, slot.Amount);
                    issuedSlots.Add(newSlot);
                }
            }

            return issuedSlots;
        }

    }

}

