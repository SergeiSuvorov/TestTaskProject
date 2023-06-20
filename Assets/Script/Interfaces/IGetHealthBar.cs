using UniRx;
using UnityEngine;

namespace Interface
{
    public interface IGetHealthBar
    {
        Vector3 Position { get; }
        bool isVisible { get; }
        string Name { get; }
        int MaxHealth { get; }
        public ReactiveProperty<int> HealthValue { get;  }
        public ReactiveCommand<IGetHealthBar> OnDeathAction { get; }
    }
}