using UniRx;

namespace Interface
{
    public interface IInventoryOpenInputController
    {
        ReactiveCommand OpenIventory { get; }
    }
}