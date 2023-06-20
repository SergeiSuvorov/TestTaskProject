using Interface;
using System;
using UniRx;
using View;

namespace Controller
{
    public sealed class InventorySlotController : BaseController, IInventorySlot
    {
        
        public bool IsFull => Amount==Capacity;

        public bool IsEmpty => Item==null;

        public IInventoryItem Item { get; private set; }

        public int ItemId => Item.Id;

        public int Amount => IsEmpty? 0:Item.Amount;

        public int Capacity { get; private set; }

        private InventorySlotView _view;

        public readonly ReactiveCommand<InventorySlotController> OnSlotChoose = new ReactiveCommand<InventorySlotController>();
        public InventorySlotController(InventorySlotView view, CompositeDisposable disposables)
        {
            _view = view;
            _view.OnSlotClick.Subscribe(_ => OnSlotChoose?.Execute(this)).AddTo(disposables);
        } 
        public void Clear()
        {
            if (IsEmpty)
                return;

            Item.Amount =0;
            Item = null;
            _view.Clear();
        }

        public void SetItem(IInventoryItem item)
        {
            Item = item;
            Capacity = item.MaxItemsInInventorySlot;
            _view.SetItem(item);
        }
    }
}
