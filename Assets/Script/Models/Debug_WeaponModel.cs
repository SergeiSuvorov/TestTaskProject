using Interface;

namespace Model
{
    public class Debug_WeaponModel : IWeaponInfo
    {
        public int MaxAmmoAmount => 4;

        public int Damage => 10;

        public float AttackDelay => 1;

        public float ReloadTime => 3;
    }
}