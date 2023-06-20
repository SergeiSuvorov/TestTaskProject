using Interface;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View
{
    public class InventorySlotView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private Image _itemImage;

        public readonly ReactiveCommand OnSlotClick=new ReactiveCommand();

        private IInventoryItem _item;

        public void SetItem(IInventoryItem item)
        {
            _item = item;
            _itemImage.sprite = item.SpriteIcon;

            SetAmount(item.Amount);       
        }

        public void Clear()
        {
            _item = null;
            _itemImage.sprite = null;
            _amountText.text = "";
        }

        public void SetAmount(int amount)
        {
            if (amount > 1)
                _amountText.text = amount.ToString();
            else
                _amountText.text = "";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnSlotClick?.Execute();
        }
    }
}