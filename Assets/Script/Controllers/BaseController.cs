using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Controller
{
    public class BaseController : IDisposable
    {
        private List<BaseController> _baseControllers = new List<BaseController>();
        private List<GameObject> _gameObjects = new List<GameObject>();
        private bool _isDisposed;

        public void DestroyController(BaseController controller)
        {
            var index = 0;
            _baseControllers.IndexOf(controller, index);

            if(index>-1)
            {
                _baseControllers.Remove(controller);
                controller?.Dispose();
            }
        }
        public void Dispose()
        {
            OnDispose();
            if (_isDisposed)
                return;

            _isDisposed = true;

            foreach (var baseController in _baseControllers)
                baseController?.Dispose();

            _baseControllers.Clear();

            foreach (var cachedGameObject in _gameObjects)
                Object.Destroy(cachedGameObject);

            _gameObjects.Clear();
        }

        protected void AddController(BaseController baseController)
        {
            _baseControllers.Add(baseController);
        }

        protected void AddGameObjects(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }
        protected TView LoadView<TView>(Transform parentGameTransform, string Path) where TView: MonoBehaviour
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(Path), parentGameTransform, false);
            AddGameObjects(objectView);

            return objectView.GetComponent<TView>();
        }
        protected virtual void OnDispose()
        { 
        }
    }
}
