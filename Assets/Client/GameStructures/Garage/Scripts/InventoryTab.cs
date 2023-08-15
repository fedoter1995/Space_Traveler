using Architecture;
using SpaceTraveler.UI;
using UnityEngine;

namespace SpaceTraveler.GameStructures.ItemCollections.UI
{
    public class InventoryTab : InteractiveTab
    {
        [SerializeField]
        private UIItemCollection _itemCollection;
        public override void Initialize()
        {
            var iemCollection = Game.GetInteractor<InventoryInteractor>().collection;
            _itemCollection.Initilize(iemCollection);
            Close();
        }

        protected override void OnOpen()
        {
            _itemCollection.UpdateSlots();
        }
    }

}

