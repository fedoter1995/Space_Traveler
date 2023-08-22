using SpaceTraveler.GameStructures.Craft;
using System.Collections.Generic;

using UnityEngine;

namespace SpaceTraveler.GameStructures.Items
{
    [CreateAssetMenu(menuName = "Item/New_Item")]
    public class Item : ScriptableObject, IHaveDescription
    {
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _maxItemsInSlot;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ItemObject _prefab;
        [SerializeField] private SlotType _slotType;
        [SerializeField] private int _inSlotCapacity;
        [SerializeField] private CraftRequirements _craftRequirements;


        public string Id => _id;
        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public ItemObject Prefab => _prefab;
        public SlotType SlotType => _slotType;
        public int MaxItemsInSlot => _maxItemsInSlot;
        public CraftRequirements Requirements => _craftRequirements;

        public virtual DescriptionData GetDescriptionData()
        {
            return new DescriptionData(_description, _name, null, _icon);
        }

        public Dictionary<string, object> GetObjectData()
        {
            var data = new Dictionary<string, object>();

            data.Add("Name", Name);
            data.Add("Id", Id);

            return data;
        }
    }

}
