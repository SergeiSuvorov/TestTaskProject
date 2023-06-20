using Interface;
using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class Item : IInventoryItem
    {
        public int Id { get ; private set ; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int MaxItemsInInventorySlot { get; private set; }
        public Sprite SpriteIcon { get; private set; }
        public bool CanEquipped { get; private set; }
        public ItemType ItemType { get; private set; }

        public bool IsEquipped { get; set; }
        public int Amount { get; set; }

        public Item(IInventoryItemInfo inventoryItemInfo, int amount=0)
        {
            Id = inventoryItemInfo.Id;
            Title = inventoryItemInfo.Title;
            Description = inventoryItemInfo.Description;
            MaxItemsInInventorySlot = inventoryItemInfo.MaxItemsInInventorySlot;
            SpriteIcon = inventoryItemInfo.SpriteIcon;
            CanEquipped = inventoryItemInfo.CanEquipped;

            Amount = amount;
            IsEquipped = false;
        }

        private Item(int id, string title, string description, int maxItemsInInventorySlot, Sprite spriteIcon, bool canEquipped, ItemType itemType, bool isEquipped, int amount)
        {
            Id = id;
            Title = title;
            Description = description;
            MaxItemsInInventorySlot = maxItemsInInventorySlot;
            SpriteIcon = spriteIcon;
            CanEquipped = canEquipped;
            ItemType = itemType;
            IsEquipped = isEquipped;
            Amount = amount;
        }

        public Item(ItemDataForSafe itemDataForSafe)
        {
            Id = itemDataForSafe.Id;
            Title = itemDataForSafe.Title;
            Description = itemDataForSafe.Description;
            MaxItemsInInventorySlot = itemDataForSafe.MaxItemsInInventorySlot;
            SpriteIcon = itemDataForSafe.SpriteIcon;
            CanEquipped = itemDataForSafe.CanEquipped;
            ItemType = itemDataForSafe.ItemType;
            IsEquipped = itemDataForSafe.IsEquipped;
            Amount = itemDataForSafe.Amount;
        }

        public IInventoryItem Clone()
        {
            var cloneItem = new Item(Id,Title,Description,MaxItemsInInventorySlot,SpriteIcon,CanEquipped,ItemType,IsEquipped,Amount);
            return cloneItem;
        }
    }
}
