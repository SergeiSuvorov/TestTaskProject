using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class ItemDataBaseDataRecord
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField,SerializeReference] private BaseItemDataRecord _item;

        public int Id => _item.Id;
        public BaseItemDataRecord ItemRecords => _item;
        public void CheckedItemType()
        {

            if ((_item == null && _itemType == ItemType.None) 
                || (_itemType == _item?.ItemType))
                return;

            _item = ItemDataRecordsFactory.GetItemDataRecords(_itemType);
        }
    }
}
