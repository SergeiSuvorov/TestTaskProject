namespace Interface
{
    public interface IInventoryItemState
    {
        bool IsEquipped { get; set; }
        int Amount { get; set; }
    }
}