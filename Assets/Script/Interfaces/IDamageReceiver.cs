using UniRx;

namespace Interface
{
    public interface IDamageReceiver
    {
       ReactiveCommand<int> GetDamage { get; }
    }
}