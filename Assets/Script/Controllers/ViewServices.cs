using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Controller
{
    public sealed class ViewServices : BaseController
    {
        private readonly Dictionary<string, ObjectPool> _viewCache
            = new Dictionary<string, ObjectPool>();

        public bool Check(string ObjectId)
        {
            return _viewCache.ContainsKey(ObjectId);
        }
        public GameObject Create(string path, string ObjectId)
        {

            if (!_viewCache.TryGetValue(ObjectId, out ObjectPool viewPool))
            {
                var prefab = Object.Instantiate(ResourceLoader.LoadPrefab(path));
                viewPool = new ObjectPool(prefab);
                AddController(viewPool);
                _viewCache[ObjectId] = viewPool;
            }

            return viewPool.GetFromPool();
        }

        public void Destroy(GameObject prefab, string ObjectId)
        {
            _viewCache[ObjectId].ReturnToPool(prefab);
        }
    }


}