using UnityEngine;

namespace Model
{
    public class WeaponModel
    {
        public readonly int ClipAmmo;
        public readonly int MinDamage;
        public readonly int MaxDamage;
        public readonly Sprite Sprite;

        public WeaponModel(int clipAmmo, int minDamage, int maxDamage, Sprite sprite)
        {
            ClipAmmo = clipAmmo;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            Sprite = sprite;
        }
    }
}