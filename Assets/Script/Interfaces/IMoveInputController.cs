using UniRx;
using UnityEngine;

namespace Interface
{
    public interface IMoveInputController
    {
        ReactiveProperty<Vector2> MoveInputProperty { get; }
    }
}