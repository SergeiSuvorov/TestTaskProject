using Interface;
using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public sealed class WeaponItem : IWeaponItem
    {
        public WeaponItem(WeaponDataForSave data, bool isEquipped, int amount)
        {
            Id = data.Id;
            Title = data.Title;
            Description = data.Description;
            MaxItemsInInventorySlot = data.MaxItemsInInventorySlot;
            SpriteIcon = data.SpriteIcon;
            CanEquipped = data.CanEquipped;
            ItemType = data.ItemType;
            Damage = data.Damage;
            MaxAmmoAmount = data.MaxAmmoAmount;
            AttackDelay = data.AttackDelay;
            ReloadTime = data.ReloadTime;
            IsEquipped = isEquipped;
            Amount = amount;
        }

        public WeaponItem(WeaponDataRecord data, bool isEquipped, int amount)
        {
            Id = data.Id;
            Title = data.Title;
            Description = data.Description;
            MaxItemsInInventorySlot = data.MaxItemsInInventorySlot;
            SpriteIcon = data.SpriteIcon;
            CanEquipped = data.CanEquipped;
            ItemType = data.ItemType;
            Damage = data.Damage;
            MaxAmmoAmount = data.MaxAmmoAmount;
            AttackDelay = data.AttackDelay;
            ReloadTime = data.ReloadTime;
            IsEquipped = isEquipped;
            Amount = amount;
        }

        private WeaponItem()
        {
        }

        public IInventoryItem Clone()
        {
            var weapon = new WeaponItem();
            weapon.Damage = Damage;
            weapon.MaxAmmoAmount = MaxAmmoAmount;
            weapon.Id = Id;
            weapon.Title = Title;
            weapon.Description = Description;
            weapon.MaxItemsInInventorySlot = MaxItemsInInventorySlot;
            weapon.CanEquipped = CanEquipped;
            weapon.AttackDelay = AttackDelay;
            weapon.ReloadTime = ReloadTime;
            weapon.Amount = Amount;
            weapon.IsEquipped = IsEquipped;
            weapon.ItemType = ItemType;

            return weapon;
        }

        public int Damage { get; private set; }
        public int MaxAmmoAmount { get; private set; }
        public float AttackDelay { get; private set; }
        public float ReloadTime { get; private set; }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int MaxItemsInInventorySlot { get; private set; }
        public Sprite SpriteIcon { get; private set; }
        public bool CanEquipped { get; private set; }
        public ItemType ItemType { get; private set; }

        public bool IsEquipped { get; set; }
        public int Amount { get; set; }

    }
}
