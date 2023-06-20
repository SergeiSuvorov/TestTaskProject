using Data;
using Interface;
using System.Collections.Generic;
using Tools;
using UniRx;
using UnityEngine;
using View;

namespace Controller
{
    public sealed class EnemyManagerController : BaseController
    {
        private readonly ViewServices _viewServices;
        private readonly List<EnemyData> _data;
        private readonly Transform _parentGameTransform;
        private readonly float _xSize;
        private readonly float _ySize;
        private readonly int _maxEnemyCount;

        private int _currentEnemyCount;

        public readonly ReactiveCommand<IGetHealthBar> onEnemyCreate;
        public readonly ReactiveCommand<Vector3> onEnemyDied;
        public readonly ReactiveCommand  onLastEnemyDied;

        public EnemyManagerController(Transform parentGameTransform, float xSize, float ySize, int maxEnemyCount, ViewServices viewServices)
        {
            onEnemyCreate = new ReactiveCommand<IGetHealthBar>();
            onEnemyDied = new ReactiveCommand<Vector3>();
            onLastEnemyDied = new ReactiveCommand();

            _parentGameTransform = parentGameTransform;
            _xSize = xSize-GameConstants.MapForEnemyCreationOffset;
            _ySize = ySize-GameConstants.MapForEnemyCreationOffset;
            _maxEnemyCount = maxEnemyCount;
            _currentEnemyCount = maxEnemyCount;
            _viewServices = viewServices;
            _data = LoadData();
        }

        public void CreateEnemys()
        {
            for (int i = 0; i < _maxEnemyCount; i++)
            {
                CreateEnemy();
            }
        }

        private void CreateEnemy()
        {
            var controller = GetRandomEnemy(_parentGameTransform);
            var xCoor = Random.Range(-_xSize, _xSize);
            var yCoor = Random.Range(-_ySize, _ySize);
            controller.ActivateEnemyInCoordinate(new Vector2(xCoor, yCoor));
            controller.OnDeathAction.Subscribe(value => { EnemyDeath(value as EnemyController); }).AddTo(controller.EnemyDisposables);

            onEnemyCreate?.Execute(controller);
            AddController(controller);
        }

        private void EnemyDeath(EnemyController controller)
        {
            _currentEnemyCount--;
            _viewServices.Destroy(controller.EnemyGameObject, controller.EnemyId);
            onEnemyDied?.Execute(controller.Position);
            DestroyController(controller);

            if(_currentEnemyCount==0)
            {
                onLastEnemyDied?.Execute();
            }
        }
        private List<EnemyData> LoadData()
        {
            var itemsDatas = Object.Instantiate(ResourceLoader.LoadObject<EnemysData>(ResourcePathConstants.EnemyDataResourcePath)).EnemyDatas;
            return itemsDatas;
        }

        private EnemyController GetRandomEnemy(Transform parentGameTransform)
        {
            EnemyView view;
            var index = Random.Range(0, _data.Count );

            var getView = _viewServices.Create(_data[index].PrefabPath, _data[index].EnemyId).TryGetComponent(out view);

            if (getView)
                return new EnemyController(parentGameTransform, view, _data[index]);
            else 
                return null;
        }
    }
}