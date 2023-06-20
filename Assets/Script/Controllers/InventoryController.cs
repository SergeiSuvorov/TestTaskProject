using Interface;
using Model;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UniRx;
using UnityEngine;
using View;
using Object = UnityEngine.Object;

namespace Controller
{
    public sealed class InventoryController : BaseController, IInventory
    {
        public int Capacity { get; set; }
        public bool IsFull => _slots.All(slot => slot.IsFull);

        private readonly ProfilePlayer _profilePlayer;

        private InventoryView _view;       
        private List<IInventorySlot> _slots;
        private IInventorySlot _currentSlot;
        

        public InventoryController(Transform placeForUi, ProfilePlayer profilePlayer, IInventoryOpenInputController inventoryOpenInputController, CompositeDisposable disposables)
        {
            _view = LoadView(placeForUi);
            _profilePlayer = profilePlayer;
  
            _view.BackButton.OnClickAsObservable().Subscribe(_ =>{ DeactiveInventory(); }).AddTo(disposables);
            _view.RemoveButton.OnClickAsObservable().Subscribe(_ => { RemoveItemFromCurrentSlot(); }).AddTo(disposables);
            _view.SetActiveRemoveButton(false);

            inventoryOpenInputController.OpenIventory.Subscribe(_ => ActivateInventory()).AddTo(disposables);

            var loadItems = profilePlayer.InventoryItems;

            Capacity = _view.InventorySlotsViews.Count;
            _slots = new List<IInventorySlot>(Capacity);
            for (int i = 0; i < Capacity; i++)
            {
                var slotController = new InventorySlotController(_view.InventorySlotsViews[i], disposables);
                slotController.OnSlotChoose.Subscribe(slot => { SetCurrentSlot(slot); }).AddTo(disposables);

                if(i<loadItems?.Count)
                slotController.SetItem(loadItems[i]);

                _slots.Add(slotController);

                AddController(slotController);
            }
        }

        private void SetCurrentSlot(IInventorySlot slot)
        {
            _currentSlot = slot;
            _view.SetActiveRemoveButton(!_currentSlot.IsEmpty);

        }
        private void DeactiveInventory()
        {
            _view.gameObject.SetActive(false);
        }
        public void ActivateInventory()
        {
            _currentSlot = null;
            _view.gameObject.SetActive(true); 
        }

        private void RemoveItemFromCurrentSlot()
        {
            _currentSlot?.Clear();
            SetCurrentSlot(_currentSlot);
            var items = GetAllItems();

            for (int i = 0; i < _slots.Count; i++)
            {
                if (i < (items?.Length))
                    _slots[i].SetItem(items[i]);
                else if (i >= (items?.Length) && !_slots[i].IsEmpty)
                {
                    _slots[i].Clear();
                }

            }
        }

        private InventoryView LoadView(Transform parentGameTransform)
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(ResourcePathConstants.InventoryResourcePath), parentGameTransform, false);
            AddGameObjects(objectView);

            return objectView.GetComponent<InventoryView>();
        }

        public IInventoryItem[] GetAllItems()
        {
            var allItems = new List<IInventoryItem>();
            
            for (int i = 0; i < _slots.Count; i++)
                if(!_slots[i].IsEmpty)
                    allItems.Add(_slots[i].Item);
            return allItems.ToArray();
        }

        public IInventoryItem[] GetAllItems(int itemId)
        {
            var allItemsOfType = new List<IInventoryItem>();
            var slotsOfType = _slots.
                FindAll(slot => !slot.IsEmpty && slot.ItemId == itemId);
            for (int i = 0; i < slotsOfType.Count; i++)
                if (!slotsOfType[i].IsEmpty)
                    allItemsOfType.Add(slotsOfType[i].Item);
            return allItemsOfType.ToArray();
        }

        public IInventoryItem GetItem(int itemId)
        {
            return _slots.Find(slot => slot.ItemId == itemId).Item;
        }

        public int GetItemAmount(int itemId)
        {
            var amount = 0;
            var slotsOfType = _slots.
                FindAll(slot => !slot.IsEmpty && slot.ItemId == itemId);
            for (int i = 0; i < slotsOfType.Count; i++)
                    amount+= slotsOfType[i].Amount;
            return amount;
        }

        public bool HasItem(int itemId, out IInventoryItem item)
        {
            item = GetItem(itemId);
            return item != null;
        }

        public void Remove(IInventoryItem item, int amount = 1)
        {
            var slotsWithItem = GetAllSlots(item.Id);
            if (slotsWithItem.Length == 0)
                return;

            var amountToRemove = amount;
            var count = slotsWithItem.Length;

            for(int i=count-1;i>0;i--)
            {
                var slot = slotsWithItem[i];
                if(slot.Amount>=amountToRemove)
                {
                    slot.Item.Amount -= amountToRemove;
                    if (slot.Amount == 0)
                        slot.Clear();
                    break;
                }

                amountToRemove -= slot.Amount;
                slot.Clear();
            }
        }

        public bool TryToAdd(IInventoryItem item)
        {
            var slotWithSameItemButNotFull = _slots.
               Find(slot => !slot.IsEmpty 
                    && slot.ItemId == item.Id 
                    && !slot.IsFull);
            if(slotWithSameItemButNotFull!=null)
            return TryAddToSlot(slotWithSameItemButNotFull, item);

            var emptySlot = _slots.
              Find(slot => slot.IsEmpty);

            if (emptySlot != null)
                return TryAddToSlot(emptySlot, item);

            return false;
        }

        private bool TryAddToSlot(IInventorySlot slot, IInventoryItem item)
        {
            var fits = slot.Amount + item.Amount <= item.MaxItemsInInventorySlot;
            var amountToAdd = fits
                ? item.Amount
                : item.MaxItemsInInventorySlot-slot.Amount;

            var amountLeft = item.Amount - amountToAdd;

            var clonedItem = item.Clone();
            clonedItem.Amount = amountToAdd;

            if (!slot.IsEmpty)
            {
                clonedItem.Amount += slot.Item.Amount;
            }

            slot.SetItem(clonedItem);

            if (amountLeft <= 0)
                return true;

            item.Amount = amountLeft;
            return TryToAdd(item);
        }

        public IInventorySlot[] GetAllSlots(int itemId)
        {
            return _slots.
                FindAll(slot => !slot.IsEmpty && slot.ItemId==itemId).ToArray();
        }

        protected override void OnDispose()
        {
            _profilePlayer.InventoryItems = GetAllItems().ToList();
            base.OnDispose();
        }
    }
}
