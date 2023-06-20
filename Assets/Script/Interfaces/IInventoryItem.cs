namespace Interface
{
    public interface IInventoryItem: IInventoryItemInfo, IInventoryItemState
    {
        IInventoryItem Clone();
    }
}