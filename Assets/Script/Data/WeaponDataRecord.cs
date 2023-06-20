using Interface;
using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class WeaponDataRecord : BaseItemDataRecord, IWeaponInfo
    {

        [SerializeField] private int _damage;
        [SerializeField] private int _maxAmmoAmount;
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _reloadTime;

        public int MaxAmmoAmount => _maxAmmoAmount;
        public int Damage => _damage;
        public float AttackDelay => _attackDelay;
        public float ReloadTime => _reloadTime;

        public WeaponDataRecord() : base(ItemType.Weapon)
        {
            _canEquipped = true;
            _maxItemsInInventorySlot = 1;
        }

    }
}
