using UniRx;

namespace Interface
{
    public interface IShootInputController
    {
        ReactiveCommand Shoot { get; }
        ReactiveProperty<string> AmmoInfoProperty { get; }
    }
}