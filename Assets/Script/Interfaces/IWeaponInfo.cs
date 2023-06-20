namespace Interface
{
    public interface IWeaponInfo  
    {
        public int MaxAmmoAmount { get; }
        public int Damage { get; }
        public float AttackDelay { get; }
        public float ReloadTime { get; }
    }
}