namespace SpaceTraveler.GameStructures.Items
{
    public interface IItemSlot : IJsonSerializable
    {
        Item CurrentItem { get; }
        string ItemName { get; }
        bool IsEmpty { get; }
        string ItemID { get; }
        int Amount { get; set; }
        int MaxCapacity { get; }

        void SetItem(Item item);
        void Clear();
    }
}
