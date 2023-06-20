using Pathfinding;
using UniRx;
using UnityEngine;

namespace Interface
{
    public interface ISeekerView
    {
        public ReactiveCommand<Transform> GetTarget { get; }
    }
}