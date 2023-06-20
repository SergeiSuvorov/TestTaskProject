using Data;
using Interface;
using UnityEngine;

namespace View
{
    public class ItemView : MonoBehaviour, IInventoryItemInfo
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private string _description;
        [SerializeField] private string _title;
        [SerializeField] private int _itemId;
        [SerializeField] private int _amount;

        [SerializeField] private Sprite _sprite;
        [SerializeField] private bool _canEquipped;
        [SerializeField] private ItemType _itemType;

        private IInventoryItemInfo _inventoryItemInfo;

        public int Id => _inventoryItemInfo.Id;
        public string Title => _inventoryItemInfo.Title;
        public string Description => _inventoryItemInfo.Description;
        public int MaxItemsInInventorySlot => _inventoryItemInfo.MaxItemsInInventorySlot;
        public Sprite SpriteIcon => _inventoryItemInfo.SpriteIcon;
        public bool CanEquipped => _inventoryItemInfo.CanEquipped;
        public int Amount => _amount;
        public ItemType ItemType => _inventoryItemInfo.ItemType;

        public void SetItem(IInventoryItemInfo inventoryItemInfo, int Amount)
        {
            _inventoryItemInfo = inventoryItemInfo;
            _spriteRenderer.sprite = inventoryItemInfo.SpriteIcon;
            _amount = Amount;
        }
    }
}