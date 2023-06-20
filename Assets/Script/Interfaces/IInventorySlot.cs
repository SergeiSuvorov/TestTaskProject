using System;

namespace Interface
{
    public interface IInventorySlot
    {
        bool IsFull { get;}
        bool IsEmpty { get;}

        IInventoryItem Item { get; }
        int ItemId { get; }
        int Amount { get; }
        int Capacity { get; }

        void SetItem(IInventoryItem item);
        void Clear();        
    }
}