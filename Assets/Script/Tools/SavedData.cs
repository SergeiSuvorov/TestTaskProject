using Data;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    [Serializable]
    public sealed class SavedData
    {
        public string Name;

        [SerializeField] public List<ItemDataForSafe> _itemDatasForSave;
        [SerializeField] public WeaponDataForSave PlayerEquippedWeapon;

      
        public void SetWeapon(IWeaponItem playerEquippedWeapon)
        {
            if(playerEquippedWeapon!=null)
            PlayerEquippedWeapon = new WeaponDataForSave(playerEquippedWeapon);
        }

        public IWeaponItem GetWeapon()
        {
            return new WeaponItem(PlayerEquippedWeapon,true,1);
        }
        public List<IInventoryItem> GetItems()
        {

            var inventoryItems = new List<IInventoryItem>();

            for (int i = 0; i < _itemDatasForSave.Count; i++)
            {
                if(_itemDatasForSave[i].Id!=0)
                inventoryItems.Add(new Item(_itemDatasForSave[i]));
            }

            return inventoryItems;
        }

        public void SetItems(List<IInventoryItem> inventoryItems)
        {
            _itemDatasForSave = new List<ItemDataForSafe>();
            if (inventoryItems != null)
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if (inventoryItems[i].Id != 0)
                    {
                        var data = new ItemDataForSafe(inventoryItems[i]);
                        _itemDatasForSave.Add(data);
                    }
                }
        }
    }
}