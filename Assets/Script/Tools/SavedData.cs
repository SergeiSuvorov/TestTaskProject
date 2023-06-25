using Interface;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    [Serializable]
    public sealed class SavedData
    {
        public string Name;

        [SerializeField] public List<SavedItemData> ItemDataIds;
        [SerializeField] public int PlayerEquippedWeaponId;

        public int GetWeaponID()
        {
            return PlayerEquippedWeaponId;
        }

        public void SetItems(List<IInventoryItem> inventoryItems, IWeaponItem weaponItem)
        {
            ItemDataIds = new List<SavedItemData>();
            if (inventoryItems != null)
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if (inventoryItems[i].Id != 0)
                    {
                        var savedItemData = new SavedItemData();
                        savedItemData.ItemId = inventoryItems[i].Id;
                        savedItemData.ItemAmount = inventoryItems[i].Amount;

                        ItemDataIds.Add(savedItemData);
                    }
                }

            if(weaponItem!=null)
                PlayerEquippedWeaponId = weaponItem.Id;
        }
    }
}