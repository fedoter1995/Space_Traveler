using SpaceTraveler.GameStructures.Items;
using UnityEngine;

namespace SpaceTraveler.GameStructures.ItemCollections.UI
{
    public class UIInventorySlot : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem _uiItem;


        public void SetSlot(IItemSlot slot)
        {
            _uiItem.SetItemSlot(slot);
        }
    }
}

