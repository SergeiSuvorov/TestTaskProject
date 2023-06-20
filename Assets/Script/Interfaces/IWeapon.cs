namespace Interface
{
    public interface IWeapon: IAutoAttackWeapon
    {
        void Attack();
    }

    public interface IAutoAttackWeapon: IDamageDealer
    {
        int AmmoAmount { get; }
        int MaxAmmoAmount { get; }
        float AttackDelay { get; }
        float ReloadTime { get; }
        bool IsReadyToAttack { get; }
        bool IsReloading { get; }
        void Reload();
    }

}