namespace Interface
{
    public interface IDamageDealer
    {
        int Damage { get; }
        void SetDamage(IDamageReceiver receiver);
    }
}