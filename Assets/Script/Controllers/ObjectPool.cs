using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class ObjectPool : BaseController
    {
        private readonly Stack<GameObject> _stack = new Stack<GameObject>();

        private readonly GameObject _prefab;
        private readonly GameObject _poolGameObject;

        private int _createObjectIndex;

        public ObjectPool(GameObject prefab)
        {
            _prefab = prefab;
            AddGameObjects(_prefab);
            _poolGameObject = new GameObject();
            _poolGameObject.name = prefab.name + " pool";

            ReturnToPool(_prefab);
        }

        public void ReturnToPool(GameObject go)
        {
            if (!_stack.Contains(go))
            {
                _stack.Push(go);
            }

            go.transform.SetParent(_poolGameObject.transform);
            go.SetActive(false);
        }

        public GameObject GetFromPool()
        {
            var go = Pop();
            go.transform.SetParent(null);

            return go;
        }

        private GameObject Pop()
        {
            GameObject go;

            if (_stack.Count == 0)
            {
                go = Object.Instantiate(_prefab);
                AddGameObjects(go);
                _createObjectIndex++;
                go.name = _prefab.name + " " + _createObjectIndex;

                if (!go.activeInHierarchy)
                {
                    go.SetActive(true);
                }

            }
            else
            {
                go = _stack.Pop();
                go.SetActive(true);
                if (!go.activeInHierarchy)
                {
                    go.SetActive(true);
                }
            }
            go.SetActive(true);

            return go;
        }
    }


}