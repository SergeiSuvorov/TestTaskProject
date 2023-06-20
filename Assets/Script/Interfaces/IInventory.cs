using System;

namespace Interface
{
    public interface IInventory 
    {
        int Capacity { get; set; }
        bool IsFull { get; }
        
        IInventoryItem GetItem(int itemId);
        IInventoryItem[] GetAllItems();
        IInventoryItem[] GetAllItems(int itemId);
        int GetItemAmount(int itemId);

        bool TryToAdd(IInventoryItem item);
        void Remove(IInventoryItem item, int amount=1);
        bool HasItem(int itemId, out IInventoryItem item);
    }
}