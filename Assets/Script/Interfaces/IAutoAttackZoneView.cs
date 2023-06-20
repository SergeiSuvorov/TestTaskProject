using UniRx;

namespace Interface
{
    public interface IAutoAttackZoneView
    {
        public ReactiveCommand<IDamageReceiver> GetAttackContact { get; }
        public ReactiveCommand<IDamageReceiver> LoseAttackContact { get; }
    }
}