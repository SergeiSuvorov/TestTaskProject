using Data;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using View;

namespace Controller
{
    public sealed class ItemManagerController : BaseController
    {
        private readonly List<ItemDataBaseDataRecord> _datas;
        private readonly Transform _parentGameTrasform;
        public ItemManagerController(Transform parentGameTrasform)
        {
            _datas=LoadData();
            _parentGameTrasform = parentGameTrasform;
        }

        public BaseItemDataRecord GetItem(int id)
        {
            var item = _datas.Find(item => item.Id == id).ItemRecords;
            return item;
        }

        private List<ItemDataBaseDataRecord> LoadData()
        {
            var itemsDatas = Object.Instantiate(ResourceLoader.LoadObject<ItemsData>(ResourcePathConstants.ItemDataResourcePath)).ItemDatas;
            return itemsDatas;
        }

        public GameObject CreateRandomItemInCoordinate(Vector3 position)
        {
            var index = Random.Range(0, _datas.Count);
            var item = _datas[index].ItemRecords;
            var go = Object.Instantiate(ResourceLoader.LoadPrefab(ResourcePathConstants.ItemViewResourcePath)) ;
            go.GetComponent<ItemView>().SetItem(item, 1);
            go.transform.SetParent(_parentGameTrasform);
            go.transform.position = position;
            return go;
        }

    }
}