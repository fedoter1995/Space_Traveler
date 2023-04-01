using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStructures.Items
{
    public interface IItemSlot : IJsonSerializable
    {
        Item CurrentItem { get; }
        bool IsEmpty { get; }
        string ItemID { get; }
        int Amount { get; set; }

        void SetItem(Item item);
        void Clear();
    }
}
