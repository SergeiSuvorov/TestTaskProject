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

        [SerializeField] public List<int> ItemDataIds;
        [SerializeField] public int PlayerEquippedWeaponId;

        public int GetWeaponID()
        {
            return PlayerEquippedWeaponId;
        }

        public List<int> GetItemsId()
        {
            return ItemDataIds;
        }

        public void SetItems(List<IInventoryItem> inventoryItems, IWeaponItem weaponItem)
        {
            ItemDataIds = new List<int>();
            if (inventoryItems != null)
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if (inventoryItems[i].Id != 0)
                    {
                        ItemDataIds.Add(inventoryItems[i].Id);
                    }
                }

            if(weaponItem!=null)
                PlayerEquippedWeaponId = weaponItem.Id;
        }
    }
}