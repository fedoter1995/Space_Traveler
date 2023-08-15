using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SpaceTraveler.GameStructures.Items;

namespace SpaceTraveler.GameStructures.ItemCollections.UI
{
    public class UIInventoryItem : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _name;
        [SerializeField]
        private TextMeshProUGUI _count;
        [SerializeField]
        private Image _icon;


        private IItemSlot itemSlot;


        public void SetItemSlot(IItemSlot slot)
        {
            itemSlot = slot;
            _name.text = itemSlot.CurrentItem.name;
            _count.text = itemSlot.Amount.ToString();
            _icon.sprite = itemSlot.CurrentItem.Icon;
        }
    }
}


