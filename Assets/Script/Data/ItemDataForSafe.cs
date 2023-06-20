using Interface;
using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class ItemDataForSafe
    {
        [SerializeField] public int Id;
        [SerializeField] public string Title;
        [SerializeField] public string Description;
        [SerializeField] public int MaxItemsInInventorySlot;
        [SerializeField] public Sprite SpriteIcon;
        [SerializeField] public bool CanEquipped;
        [SerializeField] public ItemType ItemType;
        [SerializeField] public bool IsEquipped;
        [SerializeField] public int Amount;


        public ItemDataForSafe(IInventoryItem inventoryItem)
        {
            Id = inventoryItem.Id;
            Title = inventoryItem.Title;
            Description = inventoryItem.Description;
            MaxItemsInInventorySlot = inventoryItem.MaxItemsInInventorySlot;
            SpriteIcon = inventoryItem.SpriteIcon;
            CanEquipped = inventoryItem.CanEquipped;
            Amount = inventoryItem.Amount;
            IsEquipped = inventoryItem.IsEquipped;
            ItemType = inventoryItem.ItemType;
        }
    }
}
