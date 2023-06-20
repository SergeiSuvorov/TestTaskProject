using Interface;
using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public abstract class BaseItemDataRecord : IInventoryItemInfo
    {
        [SerializeField] protected int _id;
        [SerializeField] protected string _title;
        [SerializeField] protected string _description;
        [SerializeField] protected int _maxItemsInInventorySlot;
        [SerializeField] protected Sprite _spriteIcon;
        [SerializeField] protected bool _canEquipped;

        protected readonly ItemType _checkedItemType;

        protected BaseItemDataRecord(ItemType checkedItemType)
        {
            _checkedItemType = checkedItemType;
        }

        public int Id => _id;
        public string Title => _title;
        public string Description => _description;
        public int MaxItemsInInventorySlot => _maxItemsInInventorySlot;
        public Sprite SpriteIcon => _spriteIcon;
        public bool CanEquipped => _canEquipped;
        public ItemType ItemType => _checkedItemType;


    }
}
