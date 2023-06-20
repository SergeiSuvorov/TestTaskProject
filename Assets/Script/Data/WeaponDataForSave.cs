using Interface;
using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class WeaponDataForSave
    {
        [SerializeField] public int Damage;
        [SerializeField] public int MaxAmmoAmount;
        [SerializeField] public float AttackDelay;
        [SerializeField] public float ReloadTime;
        [SerializeField] public int Id;
        [SerializeField] public string Title;
        [SerializeField] public string Description;
        [SerializeField] public int MaxItemsInInventorySlot;
        [SerializeField] public Sprite SpriteIcon;
        [SerializeField] public bool CanEquipped;
        [SerializeField] public ItemType ItemType;


        public WeaponDataForSave(IWeaponItem weaponData)
        {
            Damage = weaponData.Damage;
            MaxAmmoAmount = weaponData.MaxAmmoAmount;
            AttackDelay = weaponData.AttackDelay;
            ReloadTime = weaponData.ReloadTime;
            Id = weaponData.Id;
            Title = weaponData.Title;
            Description = weaponData.Description;
            MaxItemsInInventorySlot = weaponData.MaxItemsInInventorySlot;
            SpriteIcon = weaponData.SpriteIcon;
            CanEquipped = weaponData.CanEquipped;
            ItemType = weaponData.ItemType;
        }
    }
}
