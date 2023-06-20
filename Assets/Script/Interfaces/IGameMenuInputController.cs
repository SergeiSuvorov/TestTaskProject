using UniRx;

namespace Interface
{
    public interface IGameMenuInputController
    {
        ReactiveCommand BackToMainMenu { get; }
    }
}