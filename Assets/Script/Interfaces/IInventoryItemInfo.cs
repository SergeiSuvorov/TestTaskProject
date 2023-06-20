using UnityEngine;

namespace Interface
{
    public interface IInventoryItemInfo
    {
        int Id { get; }
        string Title { get; }
        string Description { get; }
        int MaxItemsInInventorySlot { get; }
        Sprite SpriteIcon { get; }
        bool CanEquipped { get; }
        ItemType ItemType { get; }
    }
}