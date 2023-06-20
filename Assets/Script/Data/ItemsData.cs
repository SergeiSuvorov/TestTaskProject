using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(ItemsData), menuName = "ItemData/" + nameof(ItemsData))]
    public sealed class ItemsData : ScriptableObject
    {
        [SerializeField] private List<ItemDataBaseDataRecord> itemDatas;
        public List<ItemDataBaseDataRecord> ItemDatas => itemDatas;

#if UNITY_EDITOR
        private void OnValidate()
        {
            for (int i = 0; i < itemDatas.Count; i++)
            {
                itemDatas[i].CheckedItemType();
            }

        }
#endif
    }
}
